--User
CREATE TABLE Summer_User(
	ID
	WxID
	CreatedDatetime
)

--Record
CREATE TABLE Summer_Record(
	ID
	UserID
	CreatedDatetime
)

--Text
CREATE TABLE Summer_Text(
	ID
	RecordID
	Content
)

--Image
CREATE TABLE Summer_Image(
	ID
	RecordID
	ImageName
	Cloud
)