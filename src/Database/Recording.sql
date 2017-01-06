CREATE
  TABLE Recording
  (
    Id            INTEGER NOT NULL ,
    Repository_Id INTEGER NOT NULL ,
    User_Id       INTEGER NOT NULL ,
    Subject_Id    INTEGER NOT NULL ,
    Name          VARCHAR (50) NOT NULL ,
    Description   VARCHAR (150) ,
    Timing        INTEGER NOT NULL ,
                  DATE DATETIME ,
    Markers IMAGE ,
    Raw_AF3 IMAGE ,
    Raw_F7 IMAGE ,
    Raw_F3 IMAGE ,
    Raw_FC5 IMAGE ,
    Raw_T7 IMAGE ,
    Raw_P7 IMAGE ,
    Raw_O1 IMAGE ,
    Raw_O2 IMAGE ,
    Raw_P8 IMAGE ,
    Raw_T8 IMAGE ,
    Raw_FC6 IMAGE ,
    Raw_F4 IMAGE ,
    Raw_F8 IMAGE ,
    Raw_AF4 IMAGE
  )
  ON "default"
GO
ALTER TABLE Recording ADD CONSTRAINT Recording_PK PRIMARY KEY CLUSTERED (Id)
WITH
  (
    ALLOW_PAGE_LOCKS = ON ,
    ALLOW_ROW_LOCKS  = ON
  )
  ON "default"
GO