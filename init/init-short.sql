USE master;
GO
CREATE DATABASE CustomerManagementDB;
GO
USE CustomerManagementDB;
GO

-- Create the Customers table if it doesn't exist
CREATE TABLE [dbo].[Customers](
    [CustomerId] NVARCHAR(50) PRIMARY KEY,
    [FirstName] NVARCHAR(100) NOT NULL,
    [LastName] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(100),
    [PhoneNumber] NVARCHAR(50),
    [StreetAddress] NVARCHAR(200),
    [City] NVARCHAR(100),
    [StateProvince] NVARCHAR(100),
    [PostalCode] NVARCHAR(20),
    [Country] NVARCHAR(100),
    [CompanyName] NVARCHAR(200),
    [CustomerType] NVARCHAR(50),
    [RegistrationDate] DATE,
    [PaymentMethod] NVARCHAR(50),
    [Notes] NVARCHAR(500),
    [Status] NVARCHAR(50)
);
GO

-- Insert sample customer data (20 records for brevity)
INSERT INTO [dbo].[Customers] (
    [CustomerId], [FirstName], [LastName], [Email], [PhoneNumber], 
    [StreetAddress], [City], [StateProvince], [PostalCode], [Country], 
    [CompanyName], [CustomerType], [RegistrationDate], [PaymentMethod], [Notes], [Status]
)
VALUES 
('CM001', 'John', 'Smith', 'john.smith@email.com', '+1-555-123-4567', '123 Main St', 'New York', 'NY', '10001', 'USA', 'ABC Corp', 'Business', '2023-01-15', 'Credit Card', 'Prefers email communication', 'Active'),
('CM002', 'Emma', 'Johnson', 'emma.j@example.com', '+1-555-234-5678', '456 Oak Ave', 'Los Angeles', 'CA', '90001', 'USA', 'Sunshine Inc', 'Business', '2023-02-22', 'Bank Transfer', 'VIP customer', 'Active'),
('CM003', 'Michael', 'Williams', 'm.williams@domain.net', '+1-555-345-6789', '789 Pine Rd', 'Chicago', 'IL', '60601', 'USA', '', 'Individual', '2023-03-10', 'PayPal', 'Responsive to promotions', 'Active'),
('CM004', 'Sophia', 'Brown', 'sophia.brown@mail.com', '+1-555-456-7890', '101 Elm Blvd', 'Houston', 'TX', '77001', 'USA', 'Brown Enterprises', 'Business', '2023-04-05', 'Credit Card', 'International shipping needs', 'Active'),
('CM005', 'James', 'Jones', 'james.jones@company.org', '+1-555-567-8901', '202 Maple Dr', 'Phoenix', 'AZ', '85001', 'USA', '', 'Individual', '2023-05-12', 'Direct Debit', 'Often requests discounts', 'Active'),
('CM006', 'Olivia', 'Garcia', 'olivia.g@inbox.com', '+1-555-678-9012', '303 Cedar Ln', 'Philadelphia', 'PA', '19101', 'USA', 'Garcia & Sons', 'Business', '2023-06-20', 'Credit Card', 'Requires detailed invoices', 'Active'),
('CM007', 'Robert', 'Miller', 'r.miller@connect.io', '+1-555-789-0123', '404 Birch Pl', 'San Antonio', 'TX', '78201', 'USA', '', 'Individual', '2023-07-07', 'PayPal', 'Prefers phone contact', 'Active'),
('CM008', 'Emily', 'Davis', 'emily.davis@network.net', '+1-555-890-1234', '505 Walnut St', 'San Diego', 'CA', '92101', 'USA', 'Davis Industries', 'Business', '2023-08-18', 'Bank Transfer', 'High-value client', 'Active'),
('CM009', 'William', 'Rodriguez', 'w.rodriguez@mail.net', '+1-555-901-2345', '606 Spruce Ave', 'Dallas', 'TX', '75201', 'USA', '', 'Individual', '2023-09-29', 'Credit Card', 'Weekend deliveries only', 'Inactive'),
('CM010', 'Ava', 'Martinez', 'ava.m@domain.com', '+1-555-012-3456', '707 Fir Dr', 'San Jose', 'CA', '95101', 'USA', 'Martinez LLC', 'Business', '2023-10-14', 'Direct Debit', 'Bulk order customer', 'Active'),
('CM011', 'Thomas', 'Hernandez', 'thomas.h@company.com', '+44-20-1234-5678', '8 Oxford St', 'London', 'Greater London', 'W1D 1BS', 'UK', 'Hernandez Global', 'Business', '2023-11-03', 'Credit Card', 'EU market specialist', 'Active'),
('CM012', 'Isabella', 'Moore', 'i.moore@mail.co.uk', '+44-20-2345-6789', '15 Baker St', 'London', 'Greater London', 'NW1 6XE', 'UK', '', 'Individual', '2023-12-21', 'PayPal', 'Returns frequently', 'Active'),
('CM013', 'Charles', 'Taylor', 'c.taylor@network.co.uk', '+44-20-3456-7890', '22 King''s Rd', 'Manchester', 'Greater Manchester', 'M60 1NW', 'UK', 'Taylor Traders', 'Business', '2024-01-09', 'Bank Transfer', 'Quarterly bulk orders', 'Active'),
('CM014', 'Mia', 'Thomas', 'mia.t@connect.co.uk', '+44-20-4567-8901', '37 Queen St', 'Edinburgh', 'Scotland', 'EH2 1JQ', 'UK', '', 'Individual', '2024-02-17', 'Credit Card', 'Seasonal shopper', 'Inactive'),
('CM015', 'Joseph', 'White', 'j.white@domain.co.uk', '+44-20-5678-9012', '44 Castle St', 'Cardiff', 'Wales', 'CF10 1BS', 'UK', 'White Innovations', 'Business', '2024-03-25', 'Direct Debit', 'Tech industry client', 'Active'),
('CM016', 'Sofia', 'Clark', 'sofia.c@email.ca', '+1-416-123-4567', '55 Bay St', 'Toronto', 'Ontario', 'M5J 2R8', 'Canada', 'Clark Solutions', 'Business', '2024-04-02', 'Credit Card', 'Needs French documentation', 'Active'),
('CM017', 'David', 'Hall', 'david.hall@inbox.ca', '+1-416-234-5678', '66 Front St W', 'Toronto', 'Ontario', 'M5J 1E6', 'Canada', '', 'Individual', '2024-05-11', 'PayPal', 'Interested in new products', 'Active'),
('CM018', 'Camila', 'Wood', 'c.wood@connect.ca', '+1-604-345-6789', '77 Robson St', 'Vancouver', 'British Columbia', 'V6B 2A1', 'Canada', 'Wood Enterprises', 'Business', '2024-06-20', 'Bank Transfer', 'Monthly subscription', 'Active'),
('CM019', 'Alexander', 'Lee', 'alex.lee@network.ca', '+1-514-456-7890', '88 Sainte-Catherine W', 'Montreal', 'Quebec', 'H3B 1E3', 'Canada', '', 'Individual', '2024-07-03', 'Credit Card', 'Bilingual communications', 'Inactive'),
('CM020', 'Victoria', 'Young', 'v.young@domain.ca', '+1-403-567-8901', '99 Stephen Ave', 'Calgary', 'Alberta', 'T2P 1K3', 'Canada', 'Young Group', 'Business', '2024-08-15', 'Direct Debit', 'Oil industry client', 'Active');

PRINT 'Customer data has been loaded successfully.';
PRINT 'Total records imported: 20';
GO
