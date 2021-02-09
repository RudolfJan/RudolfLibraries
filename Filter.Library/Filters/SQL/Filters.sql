BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Categories" (
	"Id"	INTEGER NOT NULL,
	"CategoryName"	TEXT NOT NULL UNIQUE ON CONFLICT IGNORE,
	"CategoryDescription"	TEXT NOT NULL,
	PRIMARY KEY("Id")
);
CREATE TABLE IF NOT EXISTS "Tags" (
	"Id"	INTEGER NOT NULL,
	"TagName"	TEXT NOT NULL UNIQUE ON CONFLICT IGNORE,
	"TagDescription"	TEXT NOT NULL,
	PRIMARY KEY("Id")
);
CREATE TABLE IF NOT EXISTS "TagCategories" (
	"Id"	INTEGER NOT NULL,
	"TagId"	INTEGER NOT NULL,
	"CategoryId"	INTEGER NOT NULL,
	PRIMARY KEY("Id"),
	FOREIGN KEY("TagId") REFERENCES "Tags"("Id") ON DELETE CASCADE,
	FOREIGN KEY("CategoryId") REFERENCES "Categories"("Id") ON DELETE CASCADE
);
CREATE VIEW IF NOT EXISTS TagsAndCategoriesView (TagId, TagName, CategoryId, CategoryName)
AS
SELECT Tags.Id as TagId, Tags.TagName as TagName, Categories.Id as CategoryId, Categories.CategoryName as CategoryName 
	FROM Tags, Categories, TagCategories 
	WHERE TagCategories.TagId= Tags.Id AND TagCategories.CategoryId= Categories.Id;
COMMIT;
