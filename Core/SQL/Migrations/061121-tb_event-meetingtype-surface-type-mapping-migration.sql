BEGIN TRAN

--SURFACE TYPE

UPDATE tb_event set surface_type = null where surface_type = ''

INSERT INTO tb_surface_type
SELECT DISTINCT surface_type FROM tb_event
where surface_type is not null

CREATE TABLE eventSurfaceMapping
(
event_id int,
surface_type varchar(50),
surface_type_id int null
)

INSERT INTO eventSurfaceMapping 
SELECT event_id, surface_type, null
FROM tb_event where surface_type is not null


UPDATE
    eventSurfaceMapping
SET
    eventSurfaceMapping.surface_type_id = RAN.surface_type_id
FROM
    eventSurfaceMapping SI
INNER JOIN
    tb_surface_type RAN
ON 
    SI.surface_type = RAN.surface_type;


UPDATE tb_event SET surface_type = null

ALTER TABLE tb_event 
ALTER COLUMN surface_type int NULL

ALTER TABLE tb_event
ADD CONSTRAINT FK_event_surface
FOREIGN KEY (surface_type) REFERENCES tb_surface_type(surface_type_id);

UPDATE
    tb_event
SET
    tb_event.surface_type = RAN.surface_type_id
FROM
    tb_event SI
INNER JOIN
    eventSurfaceMapping RAN
ON 
    SI.event_id = RAN.event_id;

drop table eventSurfaceMapping

--MEETING TYPE

INSERT INTO tb_meeting_type
SELECT DISTINCT meeting_type FROM tb_event

CREATE TABLE meetingTypeMapping
(
event_id int,
meeting_type varchar(50),
meeting_type_id int null
)

INSERT INTO meetingTypeMapping 
SELECT event_id, meeting_type, null
FROM tb_event where meeting_type is not null


UPDATE
    meetingTypeMapping
SET
    meetingTypeMapping.meeting_type_id = RAN.meeting_type_id
FROM
    meetingTypeMapping SI
INNER JOIN
    tb_meeting_type RAN
ON 
    SI.meeting_type = RAN.meeting_type;


UPDATE tb_event SET meeting_type = null

ALTER TABLE tb_event 
ALTER COLUMN meeting_type int NULL

ALTER TABLE tb_event
ADD CONSTRAINT FK_event_meeting
FOREIGN KEY (meeting_type) REFERENCES tb_meeting_type(meeting_type_id);

UPDATE
    tb_event
SET
    tb_event.meeting_type = RAN.meeting_type_id
FROM
    tb_event SI
INNER JOIN
    meetingTypeMapping RAN
ON 
    SI.event_id = RAN.event_id;

drop table meetingTypeMapping

COMMIT
