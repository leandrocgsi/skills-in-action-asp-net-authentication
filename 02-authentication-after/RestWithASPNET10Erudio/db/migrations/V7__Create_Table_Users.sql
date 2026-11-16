CREATE TABLE users (
    id                        BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    user_name                 VARCHAR(255)     NULL,
    full_name                 VARCHAR(255)     NULL,
    password                  VARCHAR(255)     NULL,
    refresh_token             VARCHAR(MAX)     NULL,
    refresh_token_expiry_time DATETIME         NULL
);
