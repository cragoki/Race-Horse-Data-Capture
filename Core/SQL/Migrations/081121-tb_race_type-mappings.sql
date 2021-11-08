BEGIN TRAN

UPDATE tb_race set weather = null, stalls = null where weather = '' AND stalls = ''

INSERT INTO tb_weather_type
SELECT DISTINCT weather FROM tb_race
where weather is not null

INSERT INTO tb_going_type
SELECT DISTINCT going FROM tb_race
where going is not null

INSERT INTO tb_stalls_type
SELECT DISTINCT stalls FROM tb_race
where stalls is not null

INSERT INTO tb_distance_type
SELECT DISTINCT distance FROM tb_race
where distance is not null

INSERT INTO tb_age_type
SELECT DISTINCT ages FROM tb_race
where ages is not null


CREATE TABLE weatherMapping
(
race_id int,
weather varchar(150),
weather_type_id int null
)

CREATE TABLE goingMapping
(
race_id int,
going varchar(150),
going_type_id int null
)

CREATE TABLE stallsMapping
(
race_id int,
stalls varchar(150),
stalls_type_id int null
)

CREATE TABLE distanceMapping
(
race_id int,
distance varchar(150),
distance_type_id int null
)

CREATE TABLE ageMapping
(
race_id int,
ages varchar(150),
ages_type_id int null
)


INSERT INTO weatherMapping 
SELECT race_id, weather, null
FROM tb_race where weather is not null

INSERT INTO goingMapping 
SELECT race_id, going, null
FROM tb_race where going is not null

INSERT INTO stallsMapping 
SELECT race_id, stalls, null
FROM tb_race where stalls is not null

INSERT INTO distanceMapping 
SELECT race_id, distance, null
FROM tb_race where distance is not null

INSERT INTO ageMapping 
SELECT race_id, ages, null
FROM tb_race where ages is not null

UPDATE
    weatherMapping
SET
    weatherMapping.weather_type_id = RAN.weather_type_id
FROM
    weatherMapping SI
INNER JOIN
    tb_weather_type RAN
ON 
    SI.weather = RAN.weather_type;

UPDATE
    goingMapping
SET
    goingMapping.going_type_id = RAN.going_type_id
FROM
    goingMapping SI
INNER JOIN
    tb_going_type RAN
ON 
    SI.going = RAN.going_type;

UPDATE
    stallsMapping
SET
    stallsMapping.stalls_type_id = RAN.stalls_type_id
FROM
    stallsMapping SI
INNER JOIN
    tb_stalls_type RAN
ON 
    SI.stalls = RAN.stalls_type;

UPDATE
    distanceMapping
SET
    distanceMapping.distance_type_id = RAN.distance_type_id
FROM
    distanceMapping SI
INNER JOIN
    tb_distance_type RAN
ON 
    SI.distance = RAN.distance_type;

UPDATE
    ageMapping
SET
    ageMapping.ages_type_id = RAN.age_type_id
FROM
    ageMapping SI
INNER JOIN
    tb_age_type RAN
ON 
    SI.ages = RAN.age_type;


UPDATE tb_race SET 
weather = null,
going = null,
stalls = null,
distance = null,
ages = null

ALTER TABLE tb_race
ALTER COLUMN weather int NULL

ALTER TABLE tb_race
ALTER COLUMN going int NULL

ALTER TABLE tb_race
ALTER COLUMN stalls int NULL

ALTER TABLE tb_race
ALTER COLUMN distance int NULL

ALTER TABLE tb_race
ALTER COLUMN ages int NULL

ALTER TABLE tb_race
ADD CONSTRAINT FK_race_weather
FOREIGN KEY (weather) REFERENCES tb_weather_type(weather_type_id);

ALTER TABLE tb_race
ADD CONSTRAINT FK_race_going
FOREIGN KEY (going) REFERENCES tb_going_type(going_type_id);

ALTER TABLE tb_race
ADD CONSTRAINT FK_race_stalls
FOREIGN KEY (stalls) REFERENCES tb_stalls_type(stalls_type_id);

ALTER TABLE tb_race
ADD CONSTRAINT FK_race_distance
FOREIGN KEY (distance) REFERENCES tb_distance_type(distance_type_id);

ALTER TABLE tb_race
ADD CONSTRAINT FK_race_ages
FOREIGN KEY (ages) REFERENCES tb_age_type(age_type_id);

UPDATE
    tb_race
SET
    tb_race.weather = RAN.weather_type_id
FROM
    tb_race SI
INNER JOIN weatherMapping RAN
ON 
    SI.race_id = RAN.race_id;

UPDATE
    tb_race
SET
    tb_race.going = RAN.going_type_id
FROM
    tb_race SI
INNER JOIN
    goingMapping RAN
ON 
    SI.race_id = RAN.race_id;

UPDATE
    tb_race
SET
    tb_race.stalls = RAN.stalls_type_id
FROM
    tb_race SI
INNER JOIN
    stallsMapping RAN
ON 
    SI.race_id = RAN.race_id;

UPDATE
    tb_race
SET
    tb_race.distance = RAN.distance_type_id
FROM
    tb_race SI
INNER JOIN
    distanceMapping RAN
ON 
    SI.race_id = RAN.race_id;

UPDATE
    tb_race
SET
    tb_race.ages = RAN.ages_type_id
FROM
    tb_race SI
INNER JOIN
    ageMapping RAN
ON 
    SI.race_id = RAN.race_id;

drop table weatherMapping
drop table goingMapping
drop table stallsMapping
drop table distanceMapping
drop table ageMapping

COMMIT