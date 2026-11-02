using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET10Erudio.Data.DTO.V1;
using RestWithASPNET10Erudio.Files.Exporters.Contract;
using RestWithASPNET10Erudio.Files.Importers.Factory;
using System.Globalization;

namespace RestWithASPNET10Erudio.Files.Exporters.Impl
{
    internal class CsvExporter : IFileExporter
    {
        public FileContentResult ExportFile(List<PersonDTO> people)
        {
            using var memoryStream = new MemoryStream();

            using (var csv = CsvWriter.Create(
                memoryStream,
                new CsvWriterOptions
                {
                    HasHeader = true,
                    FormatProvider = CultureInfo.InvariantCulture,
                    WriteByteOrderMark = true,
                },
                leaveOpen: true))
            {
                csv.WriteRecord(
                    "Id", "FirstName", "LastName", "Address", "Gender", "Enabled");

                foreach (var person in people)
                {
                    csv.WriteRecord(
                        person.Id.ToString(CultureInfo.InvariantCulture),
                        person.FirstName,
                        person.LastName,
                        person.Address,
                        person.Gender,
                        person.Enabled.ToString());
                }
            }

            var fileBytes = memoryStream.ToArray();

            return new FileContentResult(fileBytes,
                MediaTypes.ApplicationCsv)
            {
                FileDownloadName = $"people_exported_{DateTime.UtcNow:yyyyMMddHHmmss}.csv"
            };
        }
    }
}
