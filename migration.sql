CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Divisions" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Divisions" PRIMARY KEY AUTOINCREMENT,
    "EnglishName" TEXT NOT NULL,
    "BanglaName" TEXT NOT NULL
);

CREATE TABLE "Districts" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Districts" PRIMARY KEY AUTOINCREMENT,
    "DivisionId" INTEGER NOT NULL,
    "EnglishName" TEXT NOT NULL,
    "BanglaName" TEXT NOT NULL,
    CONSTRAINT "FK_Division_Districts" FOREIGN KEY ("DivisionId") REFERENCES "Divisions" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "SubDistricts" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_SubDistricts" PRIMARY KEY AUTOINCREMENT,
    "DistrictId" INTEGER NOT NULL,
    "EnglishName" TEXT NOT NULL,
    "BanglaName" TEXT NOT NULL,
    CONSTRAINT "FK_District_SubDistricts" FOREIGN KEY ("DistrictId") REFERENCES "Districts" ("Id") ON DELETE RESTRICT
);

CREATE INDEX "IX_Districts_DivisionId" ON "Districts" ("DivisionId");

CREATE UNIQUE INDEX "IX_Districts_EnglishName_DivisionId" ON "Districts" ("EnglishName", "DivisionId");

CREATE UNIQUE INDEX "IX_Divisions_BanglaName" ON "Divisions" ("BanglaName");

CREATE UNIQUE INDEX "IX_Divisions_EnglishName" ON "Divisions" ("EnglishName");

CREATE INDEX "IX_SubDistricts_DistrictId" ON "SubDistricts" ("DistrictId");

CREATE UNIQUE INDEX "IX_SubDistricts_EnglishName_DistrictId" ON "SubDistricts" ("EnglishName", "DistrictId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20231211185728_initial migration', '8.0.0');

COMMIT;

