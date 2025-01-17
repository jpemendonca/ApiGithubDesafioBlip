CREATE TABLE Logs (
                      Id INTEGER PRIMARY KEY AUTOINCREMENT,
                      Date DATETIME NOT NULL,
                      Message TEXT NOT NULL,
                      Level INTEGER NOT NULL,
                      Source TEXT NOT NULL
);
