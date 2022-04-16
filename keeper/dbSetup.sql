CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vaults(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  name TEXT NOT NULL,
  description TEXT,
  isPrivate TINYINT NOT NULL DEFAULT 0,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
)default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS keeps(
  id INT AUTO_INCREMENT NOT NULL primary key,
  name TEXT NOT NULL,
  description VARCHAR(255) NOT NULL,
  img TEXT NOT NULL,
  views INT DEFAULT 0,
  kept INT DEFAULT 0,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
)default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vaultkeeps(
  id INT AUTO_INCREMENT NOT NULL primary key,
  keepId INT NOT NULL,
  vaultId INT NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
)default charset utf8 COMMENT '';

DROP TABLE IF EXISTS keeps;
DROP TABLE IF EXISTS vaults;
DROP TABLE IF EXISTS vaultkeeps;


SELECT * FROM keeps;
SELECT * FROM vaults;

SELECT * FROM accounts;
SELECT * FROM vaultkeeps;


INSERT INTO keeps
(name, description, img, creatorId)
VALUES 
("Cow", "This is a cute cow", "https://images.unsplash.com/photo-1546445317-29f4545e9d53?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=702&q=80", "6234f2ff8e822c2a6080865b" );

SELECT 
  vk.*,
  k.*
  FROM vaultkeeps vk
  JOIN keeps k on k.id = vk.keepId
  WHERE vk.vaultId = @id;