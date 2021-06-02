DECLARE @blogid uniqueidentifier  
SET @blogid = 'E852C0F8-0AD8-4DBE-B53B-9B10CD1E01F5'  
INSERT INTO Blogs (Id, Url) VALUES(@blogid, 'https://blog1.com')
INSERT INTO Posts (Id, Title, Content, BlogId) VALUES (NEWID(), 'Post 1', 'Test', @blogid)