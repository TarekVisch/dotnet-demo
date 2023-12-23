CREATE TABLE Users
(
    id INT PRIMARY KEY IDENTITY(1,1),
    username NVARCHAR(255) NOT NULL,
    email NVARCHAR(255) NOT NULL,
    password NVARCHAR(255) NOT NULL,
    role NVARCHAR(50) DEFAULT 'user',
    created_at DATETIME NOT NULL,
    updated_at DATETIME NOT NULL
);

CREATE TABLE Products
(
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(255) NOT NULL,
    price FLOAT NOT NULL,
    installation FLOAT NOT NULL,
    created_at DATETIME NOT NULL,
    updated_at DATETIME NOT NULL
);

CREATE TABLE Orders
(
    id INT PRIMARY KEY IDENTITY(1,1),
    client_id INT NOT NULL,
    product_id INT NOT NULL,
    width FLOAT NOT NULL,
    height FLOAT NOT NULL,
    created_at DATETIME NOT NULL,
    updated_at DATETIME NOT NULL,
    FOREIGN KEY (client_id) REFERENCES Users(id),
    FOREIGN KEY (product_id) REFERENCES Products(id)
);

INSERT INTO Products (name, price, installation, created_at, updated_at)
VALUES
    ('Commercial Carpet', 1.29, 2.00, GETDATE(), GETDATE()),
    ('Quality Carpets', 3.99, 2.50, GETDATE(), GETDATE()),
    ('Hardwood Flooring', 3.49, 3.25, GETDATE(), GETDATE()),
    ('Floating Floor', 1.99, 2.25, GETDATE(), GETDATE()),
    ('Ceramic', 1.49, 3.25, GETDATE(), GETDATE());

INSERT INTO Users (username, email, password, role, created_at, updated_at)
VALUES 
	('admin', 'admin@email.com', '123456', 'admin', GETDATE(), GETDATE()),
	('tarek', 'tarek@email.com', '123456', 'user', GETDATE(), GETDATE());