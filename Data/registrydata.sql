-- phpMyAdmin SQL Dump
-- version 4.7.0
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2017. Sze 11. 07:20
-- Kiszolgáló verziója: 10.1.26-MariaDB
-- PHP verzió: 7.1.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `registrydata`
--
CREATE DATABASE IF NOT EXISTS `registrydata` DEFAULT CHARACTER SET utf8 COLLATE utf8_hungarian_ci;
USE `registrydata`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `contacts`
--

CREATE TABLE `contacts` (
  `ID` int(11) NOT NULL,
  `Name` varchar(40) COLLATE utf8_hungarian_ci DEFAULT NULL,
  `Phone` varchar(15) COLLATE utf8_hungarian_ci NOT NULL,
  `Email` varchar(100) COLLATE utf8_hungarian_ci NOT NULL,
  `Status` int(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `contacts`
--

INSERT INTO `contacts` (`ID`, `Name`, `Phone`, `Email`, `Status`) VALUES
(1, 'Teszt Géza', '06309999999', 'teszt.geza@examle.com', 0),
(2, 'Alap Béla', '06709999999', 'alap.bela@examle.com', 0);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `county`
--

CREATE TABLE `county` (
  `ID` int(11) NOT NULL,
  `CountyName` varchar(30) COLLATE utf8_hungarian_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `county`
--

INSERT INTO `county` (`ID`, `CountyName`) VALUES
(1, 'Bács-Kiskun megye'),
(2, 'Baranya megye'),
(3, 'Békés megye'),
(4, 'Borsod-Abaúj-Zemplén megye'),
(5, 'Csongrád megye'),
(6, 'Fejér megye'),
(7, 'Győr-Moson-Sopron megye'),
(8, 'Hajdú-Bihar megye'),
(9, 'Heves megye'),
(10, 'Jász-Nagykun-Szolnok megye'),
(11, 'Komárom-Esztergom megye'),
(12, 'Nógrád megye'),
(13, 'Pest megye'),
(14, 'Somogy megye'),
(15, 'Szabolcs-Szatmár-Bereg megye'),
(16, 'Tolna megye'),
(17, 'Vas megye'),
(18, 'Veszprém megye'),
(19, 'Zala megye'),
(20, 'Budapest');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `parameters`
--

CREATE TABLE `parameters` (
  `ID` int(11) NOT NULL,
  `ParameterName` varchar(40) COLLATE utf8_hungarian_ci NOT NULL,
  `ParameterValue` varchar(20) COLLATE utf8_hungarian_ci NOT NULL,
  `Comment` varchar(60) COLLATE utf8_hungarian_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `partners`
--

CREATE TABLE `partners` (
  `ID` int(11) NOT NULL,
  `Name` varchar(80) COLLATE utf8_hungarian_ci DEFAULT NULL,
  `TypeId` int(11) DEFAULT NULL,
  `CountyId` int(11) DEFAULT NULL,
  `ZipCode` int(4) NOT NULL,
  `City` varchar(40) COLLATE utf8_hungarian_ci NOT NULL,
  `Address` varchar(60) COLLATE utf8_hungarian_ci NOT NULL,
  `OwnerId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `partners`
--

INSERT INTO `partners` (`ID`, `Name`, `TypeId`, `CountyId`, `ZipCode`, `City`, `Address`, `OwnerId`) VALUES
(1, 'Teszt Kft', 1, 20, 1114, '', '', 2),
(2, 'SDC Bt', 3, 13, 2145, 'Kerepes', '', 1);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `partnertype`
--

CREATE TABLE `partnertype` (
  `ID` int(11) NOT NULL,
  `Name` varchar(40) COLLATE utf8_hungarian_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `partnertype`
--

INSERT INTO `partnertype` (`ID`, `Name`) VALUES
(1, 'Beszállítók'),
(2, 'Kereskedések'),
(3, 'Partnerek/Vásárlók');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `switch`
--

CREATE TABLE `switch` (
  `ID` int(11) NOT NULL,
  `PartnerId` int(11) NOT NULL,
  `ContactId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `users`
--

CREATE TABLE `users` (
  `ID` int(11) NOT NULL,
  `Name` varchar(30) COLLATE utf8_hungarian_ci NOT NULL,
  `Password` varchar(32) COLLATE utf8_hungarian_ci NOT NULL,
  `Status` tinyint(1) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `contacts`
--
ALTER TABLE `contacts`
  ADD PRIMARY KEY (`ID`);

--
-- A tábla indexei `county`
--
ALTER TABLE `county`
  ADD PRIMARY KEY (`ID`);

--
-- A tábla indexei `parameters`
--
ALTER TABLE `parameters`
  ADD PRIMARY KEY (`ID`);

--
-- A tábla indexei `partners`
--
ALTER TABLE `partners`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `TypeId` (`TypeId`),
  ADD KEY `CountyId` (`CountyId`),
  ADD KEY `OwnerId` (`OwnerId`);

--
-- A tábla indexei `partnertype`
--
ALTER TABLE `partnertype`
  ADD PRIMARY KEY (`ID`);

--
-- A tábla indexei `switch`
--
ALTER TABLE `switch`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `PartnerId` (`PartnerId`),
  ADD KEY `ContactId` (`ContactId`);

--
-- A tábla indexei `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`ID`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `contacts`
--
ALTER TABLE `contacts`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT a táblához `county`
--
ALTER TABLE `county`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;
--
-- AUTO_INCREMENT a táblához `parameters`
--
ALTER TABLE `parameters`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT a táblához `partners`
--
ALTER TABLE `partners`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT a táblához `partnertype`
--
ALTER TABLE `partnertype`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT a táblához `switch`
--
ALTER TABLE `switch`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT a táblához `users`
--
ALTER TABLE `users`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;
--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `partners`
--
ALTER TABLE `partners`
  ADD CONSTRAINT `partners_ibfk_1` FOREIGN KEY (`TypeId`) REFERENCES `partnertype` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `partners_ibfk_2` FOREIGN KEY (`CountyId`) REFERENCES `county` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `partners_ibfk_3` FOREIGN KEY (`OwnerId`) REFERENCES `contacts` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Megkötések a táblához `switch`
--
ALTER TABLE `switch`
  ADD CONSTRAINT `switch_ibfk_1` FOREIGN KEY (`PartnerId`) REFERENCES `partners` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `switch_ibfk_2` FOREIGN KEY (`ContactId`) REFERENCES `contacts` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
