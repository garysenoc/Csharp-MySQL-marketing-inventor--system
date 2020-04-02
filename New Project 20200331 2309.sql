-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.5.16


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema dbmarketing2
--

CREATE DATABASE IF NOT EXISTS dbmarketing2;
USE dbmarketing2;

--
-- Definition of table `dbcustomerid`
--

DROP TABLE IF EXISTS `dbcustomerid`;
CREATE TABLE `dbcustomerid` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `customerID` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `dbcustomerid`
--

/*!40000 ALTER TABLE `dbcustomerid` DISABLE KEYS */;
INSERT INTO `dbcustomerid` (`id`,`customerID`) VALUES 
 (1,'6');
/*!40000 ALTER TABLE `dbcustomerid` ENABLE KEYS */;


--
-- Definition of table `tbadded`
--

DROP TABLE IF EXISTS `tbadded`;
CREATE TABLE `tbadded` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `productname` varchar(45) NOT NULL,
  `brand` varchar(45) NOT NULL,
  `quantity` varchar(45) NOT NULL,
  `price` varchar(45) NOT NULL,
  `delivered` varchar(45) NOT NULL,
  `date` varchar(45) NOT NULL,
  `time` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tbadded`
--

/*!40000 ALTER TABLE `tbadded` DISABLE KEYS */;
INSERT INTO `tbadded` (`id`,`productname`,`brand`,`quantity`,`price`,`delivered`,`date`,`time`) VALUES 
 (1,'fafa','agaga','12','3','asdfasf','23/10/18','03:29:25 AM'),
 (2,'toothpase','colgate','10','12','ncccc','23/10/18','01:21:28 PM'),
 (3,'Adobo','ulam','5','20','rob','23/10/18','07:03:35 PM');
/*!40000 ALTER TABLE `tbadded` ENABLE KEYS */;


--
-- Definition of table `tbadmin`
--

DROP TABLE IF EXISTS `tbadmin`;
CREATE TABLE `tbadmin` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `username` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tbadmin`
--

/*!40000 ALTER TABLE `tbadmin` DISABLE KEYS */;
INSERT INTO `tbadmin` (`id`,`username`,`password`) VALUES 
 (1,'admin','admin');
/*!40000 ALTER TABLE `tbadmin` ENABLE KEYS */;


--
-- Definition of table `tborder`
--

DROP TABLE IF EXISTS `tborder`;
CREATE TABLE `tborder` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `productname` varchar(100) NOT NULL,
  `brand` varchar(100) NOT NULL,
  `quantity` int(10) unsigned NOT NULL,
  `price` double NOT NULL,
  `totalPrice` double NOT NULL,
  `customerid` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tborder`
--

/*!40000 ALTER TABLE `tborder` DISABLE KEYS */;
INSERT INTO `tborder` (`id`,`productname`,`brand`,`quantity`,`price`,`totalPrice`,`customerid`) VALUES 
 (1,'toothpase','colgate',2,12,24,'6');
/*!40000 ALTER TABLE `tborder` ENABLE KEYS */;


--
-- Definition of table `tbproduct`
--

DROP TABLE IF EXISTS `tbproduct`;
CREATE TABLE `tbproduct` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `productname` varchar(45) NOT NULL,
  `brand` varchar(45) NOT NULL,
  `quantity` varchar(45) NOT NULL,
  `price` double DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tbproduct`
--

/*!40000 ALTER TABLE `tbproduct` DISABLE KEYS */;
INSERT INTO `tbproduct` (`id`,`productname`,`brand`,`quantity`,`price`) VALUES 
 (1,'VIVO11','VIVO','0',1000),
 (2,'F11','OPPO','0',1230.32),
 (3,'J5','SAMSUNG','0',920),
 (4,'J7','SAMSUNG','0',239),
 (5,'S6','Cherry Mobile','0',12.33),
 (7,'fafa','agaga','0',3),
 (8,'toothpase','colgate','8',12),
 (9,'Adobo','ulam','1',20);
/*!40000 ALTER TABLE `tbproduct` ENABLE KEYS */;


--
-- Definition of table `tbtransactions`
--

DROP TABLE IF EXISTS `tbtransactions`;
CREATE TABLE `tbtransactions` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `customername` varchar(100) NOT NULL,
  `product` varchar(45) NOT NULL,
  `brand` varchar(45) NOT NULL,
  `quantity` varchar(45) NOT NULL,
  `price` varchar(45) NOT NULL,
  `totalprice` varchar(45) NOT NULL,
  `date` varchar(45) NOT NULL,
  `time` varchar(45) NOT NULL,
  `transaction` varchar(45) NOT NULL,
  `customerid` varchar(45) NOT NULL,
  `cashreceive` varchar(45) NOT NULL,
  `changes` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=122 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `tbtransactions`
--

/*!40000 ALTER TABLE `tbtransactions` DISABLE KEYS */;
INSERT INTO `tbtransactions` (`id`,`customername`,`product`,`brand`,`quantity`,`price`,`totalprice`,`date`,`time`,`transaction`,`customerid`,`cashreceive`,`changes`) VALUES 
 (102,'von','F11','OPPO','2','1230.32','2460.64','23/10/18','01:20:40 PM','Ordered Products','1','6','0'),
 (103,'von','VIVO11','VIVO','2','1000','2000','23/10/18','01:20:40 PM','Ordered Products','1','6','0'),
 (104,'von','J7','SAMSUNG','4','239','956','23/10/18','01:20:40 PM','Ordered Products','1','6','0'),
 (105,'Gary','VIVO11','VIVO','3','1000','3000','23/10/18','07:02:26 PM','Ordered Products','2','5700','0.0399999999999636'),
 (106,'Gary','F11','OPPO','1','1230.32','1230.32','23/10/18','07:02:26 PM','Ordered Products','2','5700','0.0399999999999636'),
 (108,'Lea','J7','SAMSUNG','2','239','478','24/10/18','01:55:38 PM','Ordered Products','3','65','4'),
 (109,'Lea','VIVO11','VIVO','1','1000','1000','24/10/18','01:55:38 PM','Ordered Products','3','65','4'),
 (110,'Lea','VIVO11','VIVO','3','1000','3000','24/10/18','01:55:38 PM','Ordered Products','3','65','4'),
 (111,'von','fafa','agaga','2','3','6','23/10/18','01:20:40 PM','Ordered Products','1','6','0');
INSERT INTO `tbtransactions` (`id`,`customername`,`product`,`brand`,`quantity`,`price`,`totalprice`,`date`,`time`,`transaction`,`customerid`,`cashreceive`,`changes`) VALUES 
 (112,'Gary','F11','OPPO','1','1230.32','1230.32','23/10/18','07:02:26 PM','Ordered Products','2','5700','0.0399999999999636'),
 (113,'Gary','F11','OPPO','2','1230.32','2460.64','23/10/18','07:02:26 PM','Ordered Products','2','5700','0.0399999999999636'),
 (114,'Gary','fafa','agaga','3','3','9','23/10/18','07:02:26 PM','Ordered Products','2','5700','0.0399999999999636'),
 (115,'Gary','VIVO11','VIVO','2','1000','2000','23/10/18','07:02:26 PM','Ordered Products','2','5700','0.0399999999999636'),
 (116,'Lea','fafa','agaga','7','3','21','24/10/18','01:55:38 PM','Ordered Products','3','65','4'),
 (117,'Lea','Adobo','ulam','2','20','40','24/10/18','01:55:38 PM','Ordered Products','3','65','4'),
 (119,'asdfsaf','Adobo','ulam','1','20','20','24/10/18','01:56:28 PM','Ordered Products','4','200','44.37'),
 (120,'asdfsaf','S6','Cherry Mobile','1','12.33','135.63','24/10/18','01:56:28 PM','Ordered Products','4','200','44.37'),
 (121,'123','Adobo','ulam','1','20','20','22/02/19','07:29:08 PM','Ordered Products','5','10000','9980');
/*!40000 ALTER TABLE `tbtransactions` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
