CREATE TABLE Logs (
                      Id INTEGER PRIMARY KEY AUTOINCREMENT,
                      Date DATETIME NOT NULL,
                      Message TEXT NOT NULL,
                      Level INTEGER NOT NULL
);

-- Esse script sql foi usado para gerar a tabela Logs, no banco de dados sqlite desafio.db incluido no projeto