using CsvHelper;
using RestWithASPNET10Erudio.Data.DTO.V1;
using RestWithASPNET10Erudio.Files.Importers.Contract;

namespace RestWithASPNET10Erudio.Files.Importers.Impl
{
    [CsvRecord]
    internal sealed partial class CsvPersonRow
    {
        [CsvColumn("first_name")]
        public string FirstName { get; set; } = "";

        [CsvColumn("last_name")]
        public string LastName { get; set; } = "";

        [CsvColumn("address")]
        public string Address { get; set; } = "";

        [CsvColumn("gender")]
        public string Gender { get; set; } = "";
    }

    internal class CsvImporter : IFileImporter
    {
        public async Task<List<PersonDTO>> ImportFileAsync(Stream fileStream)
        {
            using var csv = CsvReader.Create(
                fileStream,
                new CsvReaderOptions
                {
                    HasHeader = true,
                    TrimWhitespace = true,
                    SkipEmptyLines = true,
                });

            var people = new List<PersonDTO>();

            await foreach (var record in csv.GetRecordsAsync<CsvPersonRow>())
            {
                var person = new PersonDTO
                {
                    FirstName = record.FirstName,
                    LastName = record.LastName,
                    Address = record.Address,
                    Gender = record.Gender,
                    Enabled = true
                };
                people.Add(person);
            }
            return people;
        }
    }
}
