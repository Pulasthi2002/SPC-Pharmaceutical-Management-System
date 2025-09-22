-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jul 03, 2025 at 09:21 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `spc_pharmacy_db`
--

-- --------------------------------------------------------

--
-- Table structure for table `drugs`
--

CREATE TABLE `drugs` (
  `drug_id` int(11) NOT NULL,
  `drug_name` varchar(255) NOT NULL,
  `generic_name` varchar(255) DEFAULT NULL,
  `manufacturer` varchar(255) NOT NULL,
  `batch_number` varchar(100) DEFAULT NULL,
  `expiry_date` date NOT NULL,
  `unit_price` decimal(10,2) NOT NULL,
  `quantity_in_stock` int(11) DEFAULT 0,
  `description` text DEFAULT NULL,
  `created_date` datetime DEFAULT current_timestamp(),
  `minimum_stock_level` int(11) DEFAULT 10,
  `maximum_stock_level` int(11) DEFAULT 1000
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `drugs`
--

INSERT INTO `drugs` (`drug_id`, `drug_name`, `generic_name`, `manufacturer`, `batch_number`, `expiry_date`, `unit_price`, `quantity_in_stock`, `description`, `created_date`, `minimum_stock_level`, `maximum_stock_level`) VALUES
(2, 'Amoxicillin 250mg', 'Amoxicillin', 'SPC Pharmaceuticals', 'AMX-2024-002', '2025-08-15', 15.75, 2600, 'Antibiotic for bacterial infections', '2025-06-22 20:48:58', 10, 1000),
(3, 'Aspirin 100mg', 'Acetylsalicylic Acid', 'SPC Pharmaceuticals', 'ASP-2024-003', '2027-03-20', 3.25, 3508, 'Blood thinner and pain reliever', '2025-06-22 20:48:58', 10, 1000),
(4, 'Metformin 500mg', 'Metformin HCl', 'SPC Pharmaceuticals', 'MET-2024-004', '2026-06-10', 8.50, 1799, 'Diabetes medication', '2025-06-22 20:48:58', 10, 1000),
(5, 'Omeprazole 20mg', 'Omeprazole', 'SPC Pharmaceuticals', 'OME-2024-005', '2025-11-25', 12.00, 1200, 'Proton pump inhibitor for acid reflux', '2025-06-22 20:48:58', 10, 1000);

-- --------------------------------------------------------

--
-- Table structure for table `manufacturing_plants`
--

CREATE TABLE `manufacturing_plants` (
  `plant_id` int(11) NOT NULL,
  `plant_name` varchar(255) NOT NULL,
  `location` varchar(255) NOT NULL,
  `plant_code` varchar(50) DEFAULT NULL,
  `manager` varchar(255) DEFAULT NULL,
  `phone` varchar(50) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `system_type` enum('WEB_BASED','WINDOWS_BASED') NOT NULL,
  `capacity` int(11) DEFAULT 0,
  `is_active` tinyint(1) DEFAULT 1,
  `established_date` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `manufacturing_plants`
--

INSERT INTO `manufacturing_plants` (`plant_id`, `plant_name`, `location`, `plant_code`, `manager`, `phone`, `email`, `system_type`, `capacity`, `is_active`, `established_date`) VALUES
(1, 'SPC Colombo Plant', 'Colombo Industrial Zone', 'SPC-COL-01', 'Mr. Kamal Rajapaksa', '011-5678901', NULL, 'WINDOWS_BASED', 10000, 1, NULL),
(2, 'SPC Kandy Plant', 'Kandy Export Processing Zone', 'SPC-KDY-02', 'Ms. Malini Wijesinghe', '081-3345678', NULL, 'WEB_BASED', 8000, 1, NULL),
(3, 'SPC Galle Plant', 'Southern Industrial Park', 'SPC-GAL-03', 'Mr. Sunil Bandara', '091-4456789', NULL, 'WEB_BASED', 6000, 1, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

CREATE TABLE `orders` (
  `order_id` int(11) NOT NULL,
  `pharmacy_id` int(11) NOT NULL,
  `order_date` datetime DEFAULT current_timestamp(),
  `status` enum('PENDING','PROCESSING','SHIPPED','DELIVERED','CANCELLED') DEFAULT 'PENDING',
  `total_amount` decimal(10,2) DEFAULT 0.00,
  `order_notes` text DEFAULT NULL,
  `shipping_address` text DEFAULT NULL,
  `payment_status` enum('PENDING','PAID','CANCELLED') DEFAULT 'PENDING'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `orders`
--

INSERT INTO `orders` (`order_id`, `pharmacy_id`, `order_date`, `status`, `total_amount`, `order_notes`, `shipping_address`, `payment_status`) VALUES
(9, 8, '2025-07-02 12:12:19', 'PENDING', 15.75, 'Urgent', 'Kandy', 'PAID'),
(10, 8, '2025-07-02 12:33:13', 'PENDING', 3250.00, 'Urgent ', 'Kandy', 'PAID'),
(11, 8, '2025-07-03 19:52:18', 'PENDING', 32500.00, 'aa', 'Kandy', 'PENDING');

-- --------------------------------------------------------

--
-- Table structure for table `order_items`
--

CREATE TABLE `order_items` (
  `order_item_id` int(11) NOT NULL,
  `order_id` int(11) NOT NULL,
  `drug_id` int(11) NOT NULL,
  `quantity` int(11) NOT NULL,
  `unit_price` decimal(10,2) NOT NULL,
  `total_price` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `order_items`
--

INSERT INTO `order_items` (`order_item_id`, `order_id`, `drug_id`, `quantity`, `unit_price`, `total_price`) VALUES
(11, 9, 2, 1, 15.75, 15.75),
(12, 10, 3, 1000, 3.25, 3250.00),
(13, 11, 3, 10000, 3.25, 32500.00);

-- --------------------------------------------------------

--
-- Table structure for table `pharmacies`
--

CREATE TABLE `pharmacies` (
  `pharmacy_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `pharmacy_name` varchar(255) NOT NULL,
  `contact_person` varchar(255) DEFAULT NULL,
  `phone` varchar(50) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `address` text DEFAULT NULL,
  `license_number` varchar(100) NOT NULL,
  `registration_date` datetime DEFAULT current_timestamp(),
  `is_active` tinyint(1) DEFAULT 1,
  `pharmacy_type` enum('SPC_OWNED','LINKED_DEALER') DEFAULT 'LINKED_DEALER'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `pharmacies`
--

INSERT INTO `pharmacies` (`pharmacy_id`, `user_id`, `pharmacy_name`, `contact_person`, `phone`, `email`, `address`, `license_number`, `registration_date`, `is_active`, `pharmacy_type`) VALUES
(8, 28, 'Hiru Medicals', 'Hiruni', '0372260663', 'h@gmail.com', 'Kurunegala ', 'PHM0112', '2025-07-02 12:05:05', 1, 'LINKED_DEALER');

-- --------------------------------------------------------

--
-- Table structure for table `proposal_items`
--

CREATE TABLE `proposal_items` (
  `proposal_item_id` int(11) NOT NULL,
  `proposal_id` int(11) NOT NULL,
  `tender_item_id` int(11) NOT NULL,
  `proposed_unit_price` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `proposal_items`
--

INSERT INTO `proposal_items` (`proposal_item_id`, `proposal_id`, `tender_item_id`, `proposed_unit_price`) VALUES
(6, 23, 2, 1.00),
(7, 24, 2, 2.00);

-- --------------------------------------------------------

--
-- Table structure for table `stock_updates`
--

CREATE TABLE `stock_updates` (
  `update_id` int(11) NOT NULL,
  `drug_id` int(11) NOT NULL,
  `quantity` int(11) NOT NULL,
  `update_type` enum('ADD','REMOVE','ADJUSTMENT') NOT NULL,
  `reason` text DEFAULT NULL,
  `update_date` datetime DEFAULT current_timestamp(),
  `updated_by` varchar(255) NOT NULL,
  `reference_number` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `stock_updates`
--

INSERT INTO `stock_updates` (`update_id`, `drug_id`, `quantity`, `update_type`, `reason`, `update_date`, `updated_by`, `reference_number`) VALUES
(1, 3, 10, 'ADD', 'hghh', '2025-06-29 14:07:06', 'hghg', NULL),
(2, 2, 10, 'ADD', 'dfgfgdf', '2025-06-29 14:08:50', 'dfgdfgdf', NULL),
(3, 2, 2500, 'REMOVE', 'fgf', '2025-06-30 00:33:04', '1', '1242325'),
(4, 2, 2500, 'ADD', 'Update', '2025-07-02 12:40:54', '1', '4512'),
(5, 2, 2600, 'ADD', 'Adding', '2025-07-03 22:19:35', '1', 'Ureni');

-- --------------------------------------------------------

--
-- Table structure for table `suppliers`
--

CREATE TABLE `suppliers` (
  `supplier_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `company_name` varchar(255) NOT NULL,
  `contact_person` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `phone` varchar(50) DEFAULT NULL,
  `address` text DEFAULT NULL,
  `license_number` varchar(100) NOT NULL,
  `registration_date` datetime DEFAULT current_timestamp(),
  `is_active` tinyint(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `suppliers`
--

INSERT INTO `suppliers` (`supplier_id`, `user_id`, `company_name`, `contact_person`, `email`, `phone`, `address`, `license_number`, `registration_date`, `is_active`) VALUES
(10, 21, 'Hiru Pvt Ltd', 'Hiruni', 'hiru@gmail.com', '0112568452', 'Colombo', 'SP001', '2025-07-01 15:21:32', 1),
(12, 23, 'Med Source ', 'Kaveesha', 'K@gmail.com', '0372260663', 'Colombo', 'PHM0112', '2025-07-02 09:47:35', 1);

-- --------------------------------------------------------

--
-- Table structure for table `tenders`
--

CREATE TABLE `tenders` (
  `tender_id` int(11) NOT NULL,
  `title` varchar(255) NOT NULL,
  `description` text DEFAULT NULL,
  `created_date` datetime DEFAULT current_timestamp(),
  `deadline_date` datetime NOT NULL,
  `status` enum('OPEN','CLOSED','CANCELLED') DEFAULT 'OPEN',
  `created_by` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tenders`
--

INSERT INTO `tenders` (`tender_id`, `title`, `description`, `created_date`, `deadline_date`, `status`, `created_by`) VALUES
(2, 'Paracetamol ', '50mg', '2025-07-01 15:01:36', '2025-07-17 15:00:19', 'OPEN', 'admin');

-- --------------------------------------------------------

--
-- Table structure for table `tender_items`
--

CREATE TABLE `tender_items` (
  `tender_item_id` int(11) NOT NULL,
  `tender_id` int(11) NOT NULL,
  `drug_name` varchar(255) NOT NULL,
  `required_quantity` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tender_items`
--

INSERT INTO `tender_items` (`tender_item_id`, `tender_id`, `drug_name`, `required_quantity`) VALUES
(2, 2, 'Paracetamol ', 1000);

-- --------------------------------------------------------

--
-- Table structure for table `tender_proposals`
--

CREATE TABLE `tender_proposals` (
  `proposal_id` int(11) NOT NULL,
  `tender_id` int(11) NOT NULL,
  `supplier_id` int(11) NOT NULL,
  `proposal_date` datetime DEFAULT current_timestamp(),
  `status` enum('PENDING','ACCEPTED','REJECTED') DEFAULT 'PENDING',
  `notes` text DEFAULT NULL,
  `total_proposed_amount` decimal(12,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tender_proposals`
--

INSERT INTO `tender_proposals` (`proposal_id`, `tender_id`, `supplier_id`, `proposal_date`, `status`, `notes`, `total_proposed_amount`) VALUES
(23, 2, 10, '2025-07-02 09:54:05', 'PENDING', '', 1000.00),
(24, 2, 12, '2025-07-02 09:55:10', 'PENDING', '', 2000.00);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `user_id` int(11) NOT NULL,
  `full_name` varchar(255) NOT NULL,
  `id_number` varchar(50) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password_hash` varchar(255) NOT NULL,
  `role` enum('SUPPLIER','SPC_STAFF','PHARMACY','CUSTOMER') NOT NULL,
  `registration_date` datetime DEFAULT current_timestamp(),
  `last_login` datetime DEFAULT NULL,
  `is_active` tinyint(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_id`, `full_name`, `id_number`, `email`, `password_hash`, `role`, `registration_date`, `last_login`, `is_active`) VALUES
(21, 'Hiruni', '200201563256', 'hiru@gmail.com', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'SUPPLIER', '2025-07-01 15:21:05', '2025-07-02 12:23:20', 1),
(23, 'Kaveesha', '1999256544512', 'k@gmail.com', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'SUPPLIER', '2025-07-02 09:47:02', '2025-07-02 09:54:57', 1),
(28, 'Hiruni', '200225631256', 'h@gmail.com', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'PHARMACY', '2025-07-02 12:04:18', '2025-07-03 19:51:45', 1),
(29, 'Hiruni', '200214563256', 'hn@gmail.com', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'SPC_STAFF', '2025-07-02 12:38:24', '2025-07-03 22:16:23', 1);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `drugs`
--
ALTER TABLE `drugs`
  ADD PRIMARY KEY (`drug_id`);

--
-- Indexes for table `manufacturing_plants`
--
ALTER TABLE `manufacturing_plants`
  ADD PRIMARY KEY (`plant_id`),
  ADD UNIQUE KEY `plant_code` (`plant_code`);

--
-- Indexes for table `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`order_id`),
  ADD KEY `pharmacy_id` (`pharmacy_id`);

--
-- Indexes for table `order_items`
--
ALTER TABLE `order_items`
  ADD PRIMARY KEY (`order_item_id`),
  ADD KEY `order_id` (`order_id`),
  ADD KEY `drug_id` (`drug_id`);

--
-- Indexes for table `pharmacies`
--
ALTER TABLE `pharmacies`
  ADD PRIMARY KEY (`pharmacy_id`),
  ADD UNIQUE KEY `license_number` (`license_number`),
  ADD KEY `fk_pharmacies_user_id` (`user_id`);

--
-- Indexes for table `proposal_items`
--
ALTER TABLE `proposal_items`
  ADD PRIMARY KEY (`proposal_item_id`),
  ADD KEY `proposal_id` (`proposal_id`),
  ADD KEY `tender_item_id` (`tender_item_id`);

--
-- Indexes for table `stock_updates`
--
ALTER TABLE `stock_updates`
  ADD PRIMARY KEY (`update_id`),
  ADD KEY `drug_id` (`drug_id`);

--
-- Indexes for table `suppliers`
--
ALTER TABLE `suppliers`
  ADD PRIMARY KEY (`supplier_id`),
  ADD UNIQUE KEY `license_number` (`license_number`),
  ADD KEY `fk_suppliers_user_id` (`user_id`);

--
-- Indexes for table `tenders`
--
ALTER TABLE `tenders`
  ADD PRIMARY KEY (`tender_id`);

--
-- Indexes for table `tender_items`
--
ALTER TABLE `tender_items`
  ADD PRIMARY KEY (`tender_item_id`),
  ADD KEY `tender_id` (`tender_id`);

--
-- Indexes for table `tender_proposals`
--
ALTER TABLE `tender_proposals`
  ADD PRIMARY KEY (`proposal_id`),
  ADD KEY `tender_id` (`tender_id`),
  ADD KEY `supplier_id` (`supplier_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`),
  ADD UNIQUE KEY `email` (`email`),
  ADD UNIQUE KEY `id_number` (`id_number`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `drugs`
--
ALTER TABLE `drugs`
  MODIFY `drug_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `manufacturing_plants`
--
ALTER TABLE `manufacturing_plants`
  MODIFY `plant_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `orders`
--
ALTER TABLE `orders`
  MODIFY `order_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT for table `order_items`
--
ALTER TABLE `order_items`
  MODIFY `order_item_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT for table `pharmacies`
--
ALTER TABLE `pharmacies`
  MODIFY `pharmacy_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `proposal_items`
--
ALTER TABLE `proposal_items`
  MODIFY `proposal_item_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT for table `stock_updates`
--
ALTER TABLE `stock_updates`
  MODIFY `update_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `suppliers`
--
ALTER TABLE `suppliers`
  MODIFY `supplier_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `tenders`
--
ALTER TABLE `tenders`
  MODIFY `tender_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `tender_items`
--
ALTER TABLE `tender_items`
  MODIFY `tender_item_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `tender_proposals`
--
ALTER TABLE `tender_proposals`
  MODIFY `proposal_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `orders`
--
ALTER TABLE `orders`
  ADD CONSTRAINT `orders_ibfk_1` FOREIGN KEY (`pharmacy_id`) REFERENCES `pharmacies` (`pharmacy_id`);

--
-- Constraints for table `order_items`
--
ALTER TABLE `order_items`
  ADD CONSTRAINT `order_items_ibfk_1` FOREIGN KEY (`order_id`) REFERENCES `orders` (`order_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `order_items_ibfk_2` FOREIGN KEY (`drug_id`) REFERENCES `drugs` (`drug_id`);

--
-- Constraints for table `pharmacies`
--
ALTER TABLE `pharmacies`
  ADD CONSTRAINT `fk_pharmacies_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`);

--
-- Constraints for table `proposal_items`
--
ALTER TABLE `proposal_items`
  ADD CONSTRAINT `proposal_items_ibfk_1` FOREIGN KEY (`proposal_id`) REFERENCES `tender_proposals` (`proposal_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `proposal_items_ibfk_2` FOREIGN KEY (`tender_item_id`) REFERENCES `tender_items` (`tender_item_id`) ON DELETE CASCADE;

--
-- Constraints for table `stock_updates`
--
ALTER TABLE `stock_updates`
  ADD CONSTRAINT `stock_updates_ibfk_1` FOREIGN KEY (`drug_id`) REFERENCES `drugs` (`drug_id`);

--
-- Constraints for table `suppliers`
--
ALTER TABLE `suppliers`
  ADD CONSTRAINT `fk_suppliers_user_id` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`);

--
-- Constraints for table `tender_items`
--
ALTER TABLE `tender_items`
  ADD CONSTRAINT `tender_items_ibfk_1` FOREIGN KEY (`tender_id`) REFERENCES `tenders` (`tender_id`) ON DELETE CASCADE;

--
-- Constraints for table `tender_proposals`
--
ALTER TABLE `tender_proposals`
  ADD CONSTRAINT `tender_proposals_ibfk_1` FOREIGN KEY (`tender_id`) REFERENCES `tenders` (`tender_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `tender_proposals_ibfk_2` FOREIGN KEY (`supplier_id`) REFERENCES `suppliers` (`supplier_id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
