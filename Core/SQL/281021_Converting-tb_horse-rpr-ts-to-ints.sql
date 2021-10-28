
BEGIN TRAN

--Change null values to literally null
--Code Change: -> Ensure any results where the value trimmed is "-" are set as null
update tb_horse set top_speed = null
where LTRIM(LTRIM(RTRIM(REPLACE(REPLACE(REPLACE(REPLACE(top_speed, CHAR(10), CHAR(32)),CHAR(13), CHAR(32)),CHAR(160), CHAR(32)),CHAR(9),CHAR(32))))) = '-'

update tb_horse set top_speed = LTRIM(LTRIM(RTRIM(REPLACE(REPLACE(REPLACE(REPLACE(top_speed, CHAR(10), CHAR(32)),CHAR(13), CHAR(32)),CHAR(160), CHAR(32)),CHAR(9),CHAR(32)))))
where top_speed IS NOT NULL

update tb_horse set rpr = null
where LTRIM(LTRIM(RTRIM(REPLACE(REPLACE(REPLACE(REPLACE(rpr, CHAR(10), CHAR(32)),CHAR(13), CHAR(32)),CHAR(160), CHAR(32)),CHAR(9),CHAR(32))))) = '-'

update tb_horse set rpr = LTRIM(LTRIM(RTRIM(REPLACE(REPLACE(REPLACE(REPLACE(rpr, CHAR(10), CHAR(32)),CHAR(13), CHAR(32)),CHAR(160), CHAR(32)),CHAR(9),CHAR(32)))))
where rpr IS NOT NULL

CREATE TABLE #tb_horse_migrations
(
horse_id int,
rp_horse_id	int,
horse_name	varchar(150),
dob	datetime,
horse_url	varchar(150),
top_speed int NULL,
rpr	int NULL
)

INSERT INTO #tb_horse_migrations 
SELECT horse_id, rp_horse_id, horse_name, dob, horse_url, CAST(top_speed AS INT), CAST(rpr AS INT)
FROM tb_horse


select * from #tb_horse_migrations

--Create new table tb_horse_migration
CREATE TABLE tb_horse_migration
(
horse_id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
rp_horse_id	int,
horse_name	varchar(150),
dob	datetime,
horse_url	varchar(150),
top_speed int NULL,
rpr	int NULL
)

SET IDENTITY_INSERT tb_horse_migration ON;  
--Insert migration data
INSERT INTO tb_horse_migration (
horse_id,
rp_horse_id,
horse_name,
dob,
horse_url,
top_speed,
rpr
)
SELECT horse_id,rp_horse_id,horse_name, dob, horse_url, top_speed , rpr FROM
#tb_horse_migrations

SET IDENTITY_INSERT dbo.tb_horse_migration OFF;  


--MANUALLY REMOVE FOREIGN KEYS RELATING TO TB_HORSE
--tb_race_horse
--drop table tb_horse
DROP TABLE tb_horse
--rename tb_horse_migration into tb_horse
EXEC sp_rename 'tb_horse_migration', 'tb_horse';


--MANUALLY ADD FOREIGN KEYS RELATING TO TB_HORSE
--tb_race_horse

DROP TABLE #tb_horse_migrations

ROLLBACK
COMMIT
