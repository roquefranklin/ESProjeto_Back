-- MySQL dump 10.13  Distrib 9.0.0, for Linux (x86_64)
--
-- Host: localhost    Database: motofreta
-- ------------------------------------------------------
-- Server version	9.0.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Geolocationcoordinates`
--

DROP TABLE IF EXISTS `Geolocationcoordinates`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Geolocationcoordinates` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `accuracy` float DEFAULT NULL,
  `altitude` float DEFAULT NULL,
  `altitudeAccuracy` float DEFAULT NULL,
  `heading` float DEFAULT NULL,
  `latitude` float DEFAULT NULL,
  `longitude` float DEFAULT NULL,
  `speed` float DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Geolocationcoordinates`
--

LOCK TABLES `Geolocationcoordinates` WRITE;
/*!40000 ALTER TABLE `Geolocationcoordinates` DISABLE KEYS */;
INSERT INTO `Geolocationcoordinates` VALUES ('08dc85b4-c451-41b0-8a8a-7e38ce25d881',1,NULL,NULL,NULL,-23.1023,-45.922,NULL);
/*!40000 ALTER TABLE `Geolocationcoordinates` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Geolocationposition`
--

DROP TABLE IF EXISTS `Geolocationposition`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Geolocationposition` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `coordsId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  `timestamp` float NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_GeolocationPosition_coordsId` (`coordsId`),
  CONSTRAINT `FK_GeolocationPosition_GeolocationCoordinates_coordsId` FOREIGN KEY (`coordsId`) REFERENCES `Geolocationcoordinates` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Geolocationposition`
--

LOCK TABLES `Geolocationposition` WRITE;
/*!40000 ALTER TABLE `Geolocationposition` DISABLE KEYS */;
INSERT INTO `Geolocationposition` VALUES ('08dc85b4-c44e-4ac8-8e97-9cd593d7cb68','08dc85b4-c451-41b0-8a8a-7e38ce25d881',1717540000000);
/*!40000 ALTER TABLE `Geolocationposition` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Stoppoints`
--

DROP TABLE IF EXISTS `Stoppoints`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Stoppoints` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `geolocalizacaoId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `latitude` float NOT NULL,
  `longitude` float NOT NULL,
  `creationDate` datetime(6) NOT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_StopPoints_geolocalizacaoId` (`geolocalizacaoId`),
  KEY `IX_StopPoints_UserId` (`UserId`),
  CONSTRAINT `FK_StopPoints_GeolocationPosition_geolocalizacaoId` FOREIGN KEY (`geolocalizacaoId`) REFERENCES `Geolocationposition` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_StopPoints_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Stoppoints`
--

LOCK TABLES `Stoppoints` WRITE;
/*!40000 ALTER TABLE `Stoppoints` DISABLE KEYS */;
INSERT INTO `Stoppoints` VALUES ('c4823c93-967d-42e8-a541-b3d8001a5e5a','','08dc85b4-c44e-4ac8-8e97-9cd593d7cb68',-23.1023,-45.922,'2024-06-05 20:11:01.363097','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d');
/*!40000 ALTER TABLE `Stoppoints` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Token`
--

DROP TABLE IF EXISTS `Token`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Token` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `token` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TipoToken` int NOT NULL,
  `Validate` datetime(6) NOT NULL,
  `IsValid` tinyint(1) NOT NULL,
  `OldRefreshToken` char(36) CHARACTER SET ascii COLLATE ascii_general_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Token_OldRefreshToken` (`OldRefreshToken`),
  KEY `IX_Token_UserId` (`UserId`),
  CONSTRAINT `FK_Token_Token_OldRefreshToken` FOREIGN KEY (`OldRefreshToken`) REFERENCES `Token` (`Id`),
  CONSTRAINT `FK_Token_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Token`
--

LOCK TABLES `Token` WRITE;
/*!40000 ALTER TABLE `Token` DISABLE KEYS */;
INSERT INTO `Token` VALUES ('0ee501d0-e2d4-4c7c-9d50-a7dc6fd2a674','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiI5ZmNmM2JmYi0wZGMxLTQxMzktODEzMC01NjE0NTA2MTA5ZGMiLCJuYmYiOjE3MjIwMzk0NjQsImV4cCI6MTcyNDYzMTQ2NCwiaWF0IjoxNzIyMDM5NDY0LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.dj5cLxkONWf9_thHDq5USn548PFClFUHNRteVEtP7-U','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-26 21:17:44.477694',1,NULL),('13758c6e-22ca-4903-87a5-79423f355352','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiIyOWRlZDJkNy00MDgzLTRmMzctOTM5NS0xYTAyMjg1NjczNmMiLCJuYmYiOjE3MTc2MjUwMDksImV4cCI6MTcyMDIxNzAwOSwiaWF0IjoxNzE3NjI1MDA5LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.BCtsuI0UIJJtRnRDZD4GPiT-MvBkk-BcjhgMBx_fRSY','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-06-05 19:03:29.752870',1,NULL),('179f5826-544f-448d-bb24-2cae347a30c9','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiJmZTA4YzUxOS01NmM1LTRlMTEtOWM0NS0xZmVmMDhmZGEzMjgiLCJuYmYiOjE3MjIxMjI1MDYsImV4cCI6MTcyNDcxNDUwNiwiaWF0IjoxNzIyMTIyNTA2LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.rFoXXdY02z1_OzTy9RU4Ce3sFZWEdZknE-ygzziT4IA','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-27 20:21:46.120693',1,NULL),('188061a8-adb0-472f-84aa-a82971617228','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiJkZTlmMzc4NS1iNGJiLTQ4OGItYWI4MC05ZjAyY2RiYWFhMmUiLCJuYmYiOjE3MTcyODgwODgsImV4cCI6MTcxOTg4MDA4OCwiaWF0IjoxNzE3Mjg4MDg4LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.KGXvdSBcxpnhJjqERYuiyO13cteJ0wOZlteVB9TcXRk','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-06-01 21:28:08.684152',1,NULL),('1c679063-d8e2-4a69-9f24-06390df7684c','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiI0OTFhZWM3ZC02MjE5LTQ2NjAtODEzMS1lM2I4OGE4MzgyNTEiLCJuYmYiOjE3MjIyMTM3NjEsImV4cCI6MTcyNDgwNTc2MSwiaWF0IjoxNzIyMjEzNzYxLCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.aAge6UWjqhCPcFYN7LGg9dRr2WaY0U6JVqigro7jIpE','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-28 21:42:41.630516',1,NULL),('1f676a95-bfc4-4a7f-8d87-b0156e31e0ce','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiJiYjIwM2ZmMC1hNDM1LTQ4MmMtYjVmZS01ZTQ4MGFhNDk5YTIiLCJuYmYiOjE3MjIyMTg2MzEsImV4cCI6MTcyNDgxMDYzMSwiaWF0IjoxNzIyMjE4NjMxLCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.fNsLB1dJbpkIo0NkP6l5Bm9yb5BqPVR4XvkKv3tfbBw','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-28 23:03:51.402191',1,NULL),('23b492ec-207b-4d79-af53-d956baf11a2f','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InRlc3RlQHRlc3RlLmNvbSIsImp0aSI6IjQ1NTU3YzQxLWFhYWQtNDJmYy04Y2JjLTlhMmIwNTE3NzBiZSIsIm5iZiI6MTcyMDc0ODI5MSwiZXhwIjoxNzIzMzQwMjkxLCJpYXQiOjE3MjA3NDgyOTEsImlzcyI6Ik1vdG9mcmV0YV9Qcm9qZWN0IiwiYXVkIjoiTW90b2ZyZXRhX1Byb2plY3QifQ.gvBP9-oklYGfihtHj5EdG3GXb8aimEl25UbGJ-PdbjY','08dca20d-8aad-49d7-85ff-b8768b3caae5',3,'2024-07-11 22:38:11.604781',1,NULL),('259676ae-95f5-46f7-9ef4-1c31a04ce20d','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiI4NWYwMWY3ZC0wOGRiLTQ3Y2MtODY5Yi01YThhMWI3MDk0MjYiLCJuYmYiOjE3MjIyMTUxMDIsImV4cCI6MTcyNDgwNzEwMiwiaWF0IjoxNzIyMjE1MTAyLCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.5xFecVlHP-3WgI9DEdKcoDNJHNGfpqCdy-ZA50CXeMQ','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-28 22:05:02.800645',1,NULL),('272f6392-366e-4a49-ba27-0c6a894a8e6e','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiI0NzFlYTRmMi0yODE3LTRmNDktYmFlZi1kYzRjY2Q5NTAxNjgiLCJuYmYiOjE3MjIxMjA0NDAsImV4cCI6MTcyNDcxMjQ0MCwiaWF0IjoxNzIyMTIwNDQwLCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.RkignNQ5csyCB4s6XmZsqBTjaFh2mGQYesH-_iZsdj4','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-27 19:47:20.836970',1,NULL),('2c1c976a-7d9f-496c-87a3-b69463962b01','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiJhY2MzZjA0Mi01NDE4LTQ4MjctYTQ0NC1hOGM2OTRiZmQyZDMiLCJuYmYiOjE3MTcyODY2MjMsImV4cCI6MTcxOTg3ODYyMywiaWF0IjoxNzE3Mjg2NjIzLCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.n3eZVDUYpkrmqxe7_FtiYTMnjqwh7JKrNj4Xh030F7U','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-06-01 21:03:43.671228',1,NULL),('30e9a34e-1fae-4d39-8980-45c0b94973f1','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InRlc3RlQHRlc3RlLmNvbSIsImp0aSI6IjdhNmJhZmU5LTkzYTQtNDEyOC1iZjBhLTViOTRkMThiNmU3NCIsIm5iZiI6MTcyMTYxMDY0NSwiZXhwIjoxNzI0MjAyNjQ1LCJpYXQiOjE3MjE2MTA2NDUsImlzcyI6Ik1vdG9mcmV0YV9Qcm9qZWN0IiwiYXVkIjoiTW90b2ZyZXRhX1Byb2plY3QifQ.8tZF5XynOu16iIjcN0rSOHZBIY3AI2W9lmQbqWCN7O4','08dca20d-8aad-49d7-85ff-b8768b3caae5',3,'2024-07-21 22:10:45.281788',1,NULL),('38ad50d5-0acc-43a2-bb4c-60c1714765fa','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiJjYzQ4YTcyMi1kZjg1LTRjN2ItYTVjNS1iNzAzZTQxNjBkODYiLCJuYmYiOjE3MjIyMTM0OTQsImV4cCI6MTcyNDgwNTQ5NCwiaWF0IjoxNzIyMjEzNDk0LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.8J1wNhzautr8GAqv_XFD4Kq01Gg03wRLVxNTmlDCL-s','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-28 21:38:14.479873',1,NULL),('38da72a0-5e22-4bcb-9b62-10ba3ae74f01','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiI0MDdjYjMxMS0yYmU4LTRlYzctODczYi05ZDQ3ZmMwZjQ1MzIiLCJuYmYiOjE3MjIxMjAxODEsImV4cCI6MTcyNDcxMjE4MSwiaWF0IjoxNzIyMTIwMTgxLCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.BYacYnOOQUyC_4TmIhooeMfsdeCsP3LR7i360wPXDJU','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-27 19:43:01.810582',1,NULL),('40e18dac-d8dc-42cd-a9c2-96da2fb523f7','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiIxZDc1ZDQzYi1lMTc0LTRmMDctODRlZi1iN2E4ODQ1NGYwMTIiLCJuYmYiOjE3MjIxMTk3OTgsImV4cCI6MTcyNDcxMTc5OCwiaWF0IjoxNzIyMTE5Nzk4LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.daOUfFjPSzHYfd4jF5AaqTQejQ1bJGFi5jnGHmoKOA8','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-27 19:36:38.696966',1,NULL),('40e35969-c933-436c-8075-b2e3134d41f4','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiIzMjhjYjNjNS0xZjgwLTRjNjItODljZS04ZTk5ZGZhZDIwMWQiLCJuYmYiOjE3MTcyODgwNTUsImV4cCI6MTcxOTg4MDA1NSwiaWF0IjoxNzE3Mjg4MDU1LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.XUr3iZctbDPsFxlBO3hPne-aYpbLietMguMs6vazQy4','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-06-01 21:27:35.885360',1,NULL),('40fc5373-e1ef-4571-bdf6-5d91557bf1fd','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiI1NjQwZGY2Yi1jNGU5LTRjNjctYWU1Ni02YzA4OWY1YjhkYTYiLCJuYmYiOjE3MjIwMzg5NDcsImV4cCI6MTcyNDYzMDk0NywiaWF0IjoxNzIyMDM4OTQ3LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.zH96u7Wmhxyoe-4lHbS17bYDWXH9UVUjlSJhXg4mL_Q','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-26 21:09:07.842983',1,NULL),('47a3f247-e8fc-46de-84af-df88acee8f03','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiIxMTIyOTIyYi1kOTNjLTRhMmQtOWY4NC0wMjk3ZGVjMzk5YjgiLCJuYmYiOjE3MTc1NDc4NjAsImV4cCI6MTcyMDEzOTg2MCwiaWF0IjoxNzE3NTQ3ODYwLCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.cfx-0fJ92xiFMd-I47DU3TdGynyI1z1WgEKi1Tl9Awc','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-06-04 21:37:40.281367',1,NULL),('4ad748f7-59e2-40c8-ae16-55e6a1dd795c','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiI4YTE0ODljYi0wODRhLTQ5N2YtODE2ZS1lNjkzNjI0ZDRjMmYiLCJuYmYiOjE3MjIxMDEwMTcsImV4cCI6MTcyNDY5MzAxNywiaWF0IjoxNzIyMTAxMDE3LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.wHLd5nIL9gLTBvNnZL6qBUg6AJFrFr5KsBFw_Ffw_xM','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-27 14:23:37.779222',1,NULL),('4f616975-43ab-4dfb-92e4-1c27d69a438f','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiJlNjcxZTc4Ny04NGZlLTQzNTktODg1YS0zZWFkN2U0ZjA4OWQiLCJuYmYiOjE3MjI2NDY3MzUsImV4cCI6MTcyNTIzODczNSwiaWF0IjoxNzIyNjQ2NzM1LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.gnV9QQuVunQ77URWruzFL90lSi5xBpXItBtlk8u4pEA','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-08-02 21:58:55.252222',1,NULL),('4fb30c37-7da3-439a-aa76-814e2804e3a1','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiI2YTgzYzgyOS01ZWI5LTQyY2UtYTAxMC01NTQyYTYyZmVlYTciLCJuYmYiOjE3MjIxMjE0ODcsImV4cCI6MTcyNDcxMzQ4NywiaWF0IjoxNzIyMTIxNDg3LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.9OqoNXKiDJXTfZBahm7nbtx98gyUV6P_G3jDC6ibmB4','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-27 20:04:47.818857',1,NULL),('51db577d-696e-4f39-84bd-ee93b2d70f58','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiIzZDU3MTlhOS02NmE1LTRhZDQtYmM2Mi0zZDc0N2Q5N2Y5MGYiLCJuYmYiOjE3MjIxMjIyOTcsImV4cCI6MTcyNDcxNDI5NywiaWF0IjoxNzIyMTIyMjk3LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.Tcq6XUxOJBectaxQJtyQlNp7W2RWVZYF6ZKBiRAhWdI','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-27 20:18:17.895567',1,NULL),('5caa1bac-8100-4b71-a710-e32e5057346a','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiIzZWU3MmQxMi01MWUxLTRmNWYtODljMy02MTZiNzUyOThmYjciLCJuYmYiOjE3MjIwMzkwNDQsImV4cCI6MTcyNDYzMTA0NCwiaWF0IjoxNzIyMDM5MDQ0LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.pItKObkRXjrdggJjrulw4I2891Rujs55FHhGTBdsH6Q','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-26 21:10:44.066962',1,NULL),('5cbbfa24-0835-4cdd-ac60-42cfc7cb9a1c','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiJjNWQxMzY5Yy1kOTQ3LTQ3NmEtYWI1Ni1hMjYyZGExZmIxNjUiLCJuYmYiOjE3MTc1NDM3MTYsImV4cCI6MTcyMDEzNTcxNiwiaWF0IjoxNzE3NTQzNzE2LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.KoMHEZF2yvlw5A74aQI65YftRQ13B4_pTYrlVmG7zSk','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-06-04 20:28:36.479146',1,NULL),('5f2b3644-0287-4d77-b653-d6161d082ce5','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiJlNDNkYTI1Mi1jYmNiLTQ4ZGMtODMxOS03OTJjZGJjM2UxM2MiLCJuYmYiOjE3MjIwMzkxMjQsImV4cCI6MTcyNDYzMTEyNCwiaWF0IjoxNzIyMDM5MTI0LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.tIsF5fByHFDUIqlaMpN9jBGTDffadsJVVEVARTuZjmw','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-26 21:12:04.249252',1,NULL),('6a2c4906-1172-4311-ba50-743f0c9355e4','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiI4MTQ1YmE2NC1mOGRiLTQxOTItOGE4NC1jYWEzN2M2YTg0MTIiLCJuYmYiOjE3MjIyMTA4NzksImV4cCI6MTcyNDgwMjg3OSwiaWF0IjoxNzIyMjEwODc5LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.T1bqFLMB_lSqxYV1dlbKjnDcUrN4cu-CruOIMVR_Ndk','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-28 20:54:39.260882',1,NULL),('6a465da9-d579-4872-91c8-b9a2819a4849','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BhdG1lLmNvbSIsImp0aSI6IjYzN2U3MWIyLTgzYjctNDFlYi1hNjQzLTZmYTUxZjY5ZmQyNyIsIm5iZiI6MTcxNDg1NDI3NywiZXhwIjoxNzE3NDQ2Mjc3LCJpYXQiOjE3MTQ4NTQyNzcsImlzcyI6Ik1vdG9mcmV0YV9Qcm9qZWN0IiwiYXVkIjoiTW90b2ZyZXRhX1Byb2plY3QifQ.X-znzPtWW4z3baEdveuQUifr_lE9aELKnQAgn3uAeME','08dc6c78-26c4-41e8-8bb3-fc8ee9590d2f',3,'2024-05-04 17:24:37.036244',1,NULL),('75cf9ac4-844d-4063-b67a-ce90162281a5','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiJjNjBlY2Y3Ny01NmU1LTQyZDUtODhiYy0xNjc2MmE3NTNiNzQiLCJuYmYiOjE3MjIwOTM4MzQsImV4cCI6MTcyNDY4NTgzNCwiaWF0IjoxNzIyMDkzODM0LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.WMqoMsnPWk-T-w8nr1-1uqi7VU0UlhrPOcOBe0ZjBTY','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-27 12:23:54.607039',1,NULL),('7daaf20b-9f2d-4ecc-9177-664b47be3754','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiJmNTE4ZmUzMC0xMjg0LTRhZjgtYjQ1Ni0wNDk1ZTAxMmM5ZTUiLCJuYmYiOjE3MjIwOTc3MDMsImV4cCI6MTcyNDY4OTcwMywiaWF0IjoxNzIyMDk3NzAzLCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.8dY6ODznloZvDb7xh-AGd9DqBCcMIIr49lDxfJEAW7k','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-27 13:28:23.754022',1,NULL),('84fe0f44-3cd2-446e-8e5a-0dc436a1fd62','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiI5NWExYThkYi0zOTgxLTQwZDktYWYwNC00N2QxYzkyMGNhNjQiLCJuYmYiOjE3MjIwOTc3NTcsImV4cCI6MTcyNDY4OTc1NywiaWF0IjoxNzIyMDk3NzU3LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0._67-SeXb933XA2dA-kT8lfBIHiSKX_KfJoZBwSbrDTA','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-27 13:29:17.621641',1,NULL),('85cfe4d6-1dc9-4041-9558-105d2c58a446','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiI0NTMzYjZlMS1jZjkxLTQwMTktOWQ1YS02M2E0MWJmYmY3Y2QiLCJuYmYiOjE3MjIxMjEzMzAsImV4cCI6MTcyNDcxMzMzMCwiaWF0IjoxNzIyMTIxMzMwLCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.UgRWNoeFGK4vaTuVtLzb8WPzcUEe4-1DBI3yUkq7-lY','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-27 20:02:10.063675',1,NULL),('89efc4f7-dbe0-46f1-b113-e5ec96ac4476','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiI3MDc4NzQwYS00N2E0LTQ2YzAtYjM4ZS04ZjY3OWNiOGQyNmQiLCJuYmYiOjE3MjIxMDA1NjgsImV4cCI6MTcyNDY5MjU2OCwiaWF0IjoxNzIyMTAwNTY4LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.xyia6t4N3EDamWIxsdQxKG3MvROdK1SqBeap2MHsin0','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-27 14:16:08.440836',1,NULL),('92922640-b6cc-4069-b60f-8489e4d14739','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiI5YzgyYTY2OS00Y2ExLTQ2MWEtODhmNS0yYWEwMDkxMmEzODYiLCJuYmYiOjE3MTc1NDM2NzcsImV4cCI6MTcyMDEzNTY3NywiaWF0IjoxNzE3NTQzNjc3LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.ss1uphIxy6xAzq7QYkpDSEZKnBpmMEOxR8cdmUwXq2M','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-06-04 20:27:57.108979',1,NULL),('96c8c7c9-ee32-4528-8353-94d0c18b1f7a','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BhdG1lLmNvbSIsImp0aSI6ImQ2ZjYwNWVjLWJiNGUtNDBmMy1hNzRiLTQ2NWJlZTQyMTBkNSIsIm5iZiI6MTcxNDg1OTIxNywiZXhwIjoxNzE3NDUxMjE3LCJpYXQiOjE3MTQ4NTkyMTcsImlzcyI6Ik1vdG9mcmV0YV9Qcm9qZWN0IiwiYXVkIjoiTW90b2ZyZXRhX1Byb2plY3QifQ.Xc-a3XP9TF83iXhdA9cJZdp0nE5Q_Ce67bM6Y7EtGU4','08dc6c78-26c4-41e8-8bb3-fc8ee9590d2f',3,'2024-05-04 18:46:57.393537',1,'6a465da9-d579-4872-91c8-b9a2819a4849'),('9d2ffb3d-737c-4ebf-9e0b-8f0cb8920ec0','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiJmZTk0YWUwNC1hZjliLTQ2NmEtOTU4My01YmZlOWQ4NzQ0MTgiLCJuYmYiOjE3MjIwNDA0MTEsImV4cCI6MTcyNDYzMjQxMSwiaWF0IjoxNzIyMDQwNDExLCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.m_EMh7VwJkmAmjkZy22ymR9YtnJlGbES2DEtgU4wTio','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-26 21:33:31.002410',1,NULL),('a7fd2605-5a4c-4b8a-9835-ac306562c664','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiIyNTVlYmJmOS1iYWFiLTQxYzMtOWMyNC04OWRjN2U4NWI2OTciLCJuYmYiOjE3MjIwNDE5NjIsImV4cCI6MTcyNDYzMzk2MiwiaWF0IjoxNzIyMDQxOTYyLCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.zQWjoopsLWT-j9Illg5ipP00KLt7VMGP8LXVXz2B2to','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-26 21:59:22.561809',1,NULL),('b50554bc-2a91-4e37-a39f-ebd91a50b804','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InRlc3RlQHRlc3RlLmNvbSIsImp0aSI6IjQxNzQzODZiLWQ5YjctNGM3ZC1iODMyLTFiMWYwM2Q4MjI3YiIsIm5iZiI6MTcyMDc0ODMwNiwiZXhwIjoxNzIzMzQwMzA2LCJpYXQiOjE3MjA3NDgzMDYsImlzcyI6Ik1vdG9mcmV0YV9Qcm9qZWN0IiwiYXVkIjoiTW90b2ZyZXRhX1Byb2plY3QifQ.swyrXH536WpikKzG7JkIUYWaTKOhoaROS5V1ofIjhSI','08dca20d-8aad-49d7-85ff-b8768b3caae5',3,'2024-07-11 22:38:26.918986',1,NULL),('b78f9542-8c43-4201-b6a3-38c7c242c259','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InRlc3RlQHRlc3RlLmNvbSIsImp0aSI6ImVkYzViYjgzLWQ5MjMtNGJiZS04MWJiLWI1MmU5NzE0M2Y2YyIsIm5iZiI6MTcyMDc0ODY4NywiZXhwIjoxNzIzMzQwNjg3LCJpYXQiOjE3MjA3NDg2ODcsImlzcyI6Ik1vdG9mcmV0YV9Qcm9qZWN0IiwiYXVkIjoiTW90b2ZyZXRhX1Byb2plY3QifQ.tIIlVUdIGKZilslmLIBh9Kcamx-gsItZLh6n1uvxEm8','08dca20d-8aad-49d7-85ff-b8768b3caae5',3,'2024-07-11 22:44:47.438740',1,NULL),('b8d798b8-b1c3-43b4-b907-ba9bae43bcbc','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiJhMjU0MmVjYi1hMWFlLTQxNDktYWY1NC03ODFkYjJmMGNmZDEiLCJuYmYiOjE3MjIwNDIwMDcsImV4cCI6MTcyNDYzNDAwNywiaWF0IjoxNzIyMDQyMDA3LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.g0bWJ1-xu7Js_fluI0jSbGfeP37MbbSY13WCU5roBiw','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-26 22:00:07.452746',1,NULL),('b93991bf-7e44-46aa-9b17-357a3ef0ef48','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InRlc3RlQHRlc3RlLmNvbSIsImp0aSI6IjMwZGNlMGU0LTU1MjctNGNkMS1iMTVlLTM1YWFlMDc1MDExOSIsIm5iZiI6MTcyMDc0NTg4NCwiZXhwIjoxNzIzMzM3ODg0LCJpYXQiOjE3MjA3NDU4ODQsImlzcyI6Ik1vdG9mcmV0YV9Qcm9qZWN0IiwiYXVkIjoiTW90b2ZyZXRhX1Byb2plY3QifQ.VxHwF9niiNmqAOPZ5bJ_e9bO364q4jbpQjtVlAtOVAs','08dca20d-8aad-49d7-85ff-b8768b3caae5',3,'2024-07-11 21:58:04.833893',1,NULL),('baee7557-3360-476a-914b-ca9f7dba7fb4','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiIwZjE2MzY1OC00NDhhLTQ4NzktYWZlZi05YjAyZmQwNjY2YTYiLCJuYmYiOjE3MTcyODc3NzgsImV4cCI6MTcxOTg3OTc3OCwiaWF0IjoxNzE3Mjg3Nzc4LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.22_nz6r4TvIGyRt1622UJX1w9-BtSYNqAdx5e4MHfJI','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-06-01 21:22:58.010338',1,NULL),('bcdd9ea1-902d-4bbf-aa88-93579e796854','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiJjZWIwMGFjMy00M2U3LTQ0MDctOTUwZC1lYjBkYmIxZmNmZjciLCJuYmYiOjE3MjIxMDAxMDYsImV4cCI6MTcyNDY5MjEwNiwiaWF0IjoxNzIyMTAwMTA2LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.yTVtiGTVuirC0SmOS7SSRFFfKQv3lr97zkn8ayl6wG0','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-27 14:08:26.304052',1,NULL),('bedcb1ad-3980-42da-9176-7c0884fd6cdd','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiJjYjU1MGRlNC1mNmEwLTQyNDAtODYxNi0wNDhmM2MwMjkzMjEiLCJuYmYiOjE3MjIwNDA4OTAsImV4cCI6MTcyNDYzMjg5MCwiaWF0IjoxNzIyMDQwODkwLCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.8ZPclnsJlcD8UnQTRX60xrUf74KsUYJs3DJ-yXKO0f4','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-26 21:41:30.517740',1,NULL),('c3d2ef24-c9e1-4d41-841b-c0e6e59f70eb','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiIzZWNjOThiYy1kYzNkLTQwMmEtYTZhNC05MGM5Mjk1NGMwOGEiLCJuYmYiOjE3MjIwMzk3NTMsImV4cCI6MTcyNDYzMTc1MywiaWF0IjoxNzIyMDM5NzUzLCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.5Z2R_35be9x8KIveT4iRO1BefeG_3Q2xjK8YRSdkjt8','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-26 21:22:33.753472',1,NULL),('cf2ed026-f11e-4ae3-a6f1-5a1c881316f1','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiIwNDA3ODA0My1jY2UyLTRlYjEtOTRjZS0zMmQ0OGM3ZGE3YzEiLCJuYmYiOjE3MjIwMzk5NTgsImV4cCI6MTcyNDYzMTk1OCwiaWF0IjoxNzIyMDM5OTU4LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.P_uCEDhBIxVL-SZEX63l97NV-lsMf_lEsebdUJ6FcKs','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-26 21:25:58.630297',1,NULL),('d50997ab-318e-4d40-a8ce-af728e741e3a','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiIyOTYwMmVkNS1jZTgyLTQ4OTgtODU0Yy04NzI5ZmFhNGZkMWQiLCJuYmYiOjE3MjIxMDA5MzYsImV4cCI6MTcyNDY5MjkzNiwiaWF0IjoxNzIyMTAwOTM2LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.oj1hP4_uPsN6e6v_Mv8aLwfURUQdwQVJ7QmjBLrjUj8','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-27 14:22:16.914955',1,NULL),('d975dfa7-5420-4d2b-b04a-1d6dd2fec52b','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiIxNDU1MThkZC0zN2M0LTRhNTItYjE2NS0xODExZjAyZGYyMTQiLCJuYmYiOjE3MTc2MjkwNDMsImV4cCI6MTcyMDIyMTA0MywiaWF0IjoxNzE3NjI5MDQzLCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.vsSWXA5nB3kis1GWr-n0ce8748rT9D6OLxDUSY1VuWI','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-06-05 20:10:43.745184',1,NULL),('e2839bec-e1ff-45dc-80cd-5d7989859e35','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiJjNjkyNWE0ZS0xOGUyLTQ3OWMtYTNkOC04MTk1ODYwNzVmNjIiLCJuYmYiOjE3MjIwOTQwNjEsImV4cCI6MTcyNDY4NjA2MSwiaWF0IjoxNzIyMDk0MDYxLCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.CTI3ffJjlkcFEsqzD-HaebvPulynvaEul8wFK7sviuY','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-27 12:27:41.727495',1,NULL),('e5f2e8bc-e3c0-4675-aa6a-be9d57ef7e55','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiI4Yzk0ZmJhYS04YjE2LTRhMmYtOThlYy0xYWYyYzdjN2M3NmUiLCJuYmYiOjE3MjIxMjU1NjAsImV4cCI6MTcyNDcxNzU2MCwiaWF0IjoxNzIyMTI1NTYwLCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.IwXjz7Bz31sTIlh5sRhIm2q6J_IX5p_MHGRNevcbDEQ','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-27 21:12:40.012141',1,NULL),('ea285700-5ad3-4696-8625-44fc21379961','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiI2NTZkODU2ZC0zMTQ5LTQ5MTgtYTNjOS1mMjUyNWZjOTBjZmUiLCJuYmYiOjE3MjIyMDIxODIsImV4cCI6MTcyNDc5NDE4MiwiaWF0IjoxNzIyMjAyMTgyLCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.DgLw0U9Xux7PivnrGu_OMX0l-MLx0StD6zkjwJCts1Q','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-28 18:29:42.912664',1,NULL),('ee6ef3db-29aa-4c74-ae64-1f17a41b11d4','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiI2NzA2MWQ1OC04YmIzLTRmZTQtYTk0OC04ODgyOGQ5MGM2MzMiLCJuYmYiOjE3MjIwOTM3NzUsImV4cCI6MTcyNDY4NTc3NSwiaWF0IjoxNzIyMDkzNzc1LCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.mqnuP-IhkKTFFDJCrEZbN6VLz_7yDLC18vBYF8S6x8w','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-27 12:22:55.369629',1,NULL),('f48f1cf7-7338-49b3-a84c-f5f7cc260565','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InRlc3RlQHRlc3RlLmNvbSIsImp0aSI6ImYzOWJkOTk3LTUzZmYtNDYxYi04MmRhLTE4ZmU2N2FlY2YzYSIsIm5iZiI6MTcyMDc0ODg4OSwiZXhwIjoxNzIzMzQwODg5LCJpYXQiOjE3MjA3NDg4ODksImlzcyI6Ik1vdG9mcmV0YV9Qcm9qZWN0IiwiYXVkIjoiTW90b2ZyZXRhX1Byb2plY3QifQ.oTfqmWBCB_NaK5qWtDr_fuV_BWW0X93K4WAMWwemHNA','08dca20d-8aad-49d7-85ff-b8768b3caae5',3,'2024-07-11 22:48:09.281840',1,NULL),('fe409a51-ab2d-4637-b47d-b5666b61a681','eyJhbGciOiJIUzI1NiIsInR5cCI6InJ0K2p3dCJ9.eyJlbWFpbCI6InN0cmluZ0BnbWFpbC5jb20iLCJqdGkiOiIyMDZlMWVmMi00YmYzLTQxNmQtYTcxMy1mN2NiMTBkZDUwZDciLCJuYmYiOjE3MjIxMjYzNzEsImV4cCI6MTcyNDcxODM3MSwiaWF0IjoxNzIyMTI2MzcxLCJpc3MiOiJNb3RvZnJldGFfUHJvamVjdCIsImF1ZCI6Ik1vdG9mcmV0YV9Qcm9qZWN0In0.vvI1LFLa3KcChjQEPVCLLM5A3tuzidPqb0EFbnsYZz4','08dc6c61-4132-48d9-86c5-f8a9fd0cd73d',3,'2024-07-27 21:26:11.731333',1,NULL);
/*!40000 ALTER TABLE `Token` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Users`
--

DROP TABLE IF EXISTS `Users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Users` (
  `Id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `Nome` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Password` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `NickName` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Perfil` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `RecoveryCode` varchar(6) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Users`
--

LOCK TABLES `Users` WRITE;
/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
INSERT INTO `Users` VALUES ('08dc6c61-4132-48d9-86c5-f8a9fd0cd73d','string','string@gmail.com','Pedro123','string','12997061940','Teste',''),('08dc6c78-26c4-41e8-8bb3-fc8ee9590d2f','Pedro Paulo','string@atme.com','string','drope','12997961940','Algum',''),('08dca20d-8aad-49d7-85ff-b8768b3caae5','Fernando','teste@teste.com','Pedro123','Morais','12991919191','Testando',''),('08dcb35c-1f76-421c-8108-8aa3db17d743','','frankroq@gmail.com','123456','frankroq',NULL,'',NULL);
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20240504173011_user info','8.0.4'),('20240527014049_Password Recovery','8.0.4'),('20240605222539_stop point','8.0.4'),('20240605230739_stop point repo','8.0.4');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-08-13 18:35:01
