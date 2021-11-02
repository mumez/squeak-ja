'From Squeak4.1 of 17 April 2010 [latest update: #9957] on 25 April 2010 at 12:43:58 am'!"Change Set:		requestDropStream-m17n-muDate:			5 February 2008Author:			Masashi UmezawaIn Squeak 3.9, 'drop & open a file' is failed when the file is dropped from the directory that contains wide string path name.This patch correctly encode native OS path name to Squeak internal one, so the file will be open. "!!StandardFileStream methodsFor: 'dnd requests' stamp: 'mu 4/25/2010 00:41'!requestDropStream: dropIndex	"Return a read-only stream for some file the user has just dropped onto Squeak."	name := (FilePath pathName: (self primDropRequestFileName: dropIndex) isEncoded: true) pathName.	fileID := self primDropRequestFileHandle: dropIndex.	fileID == nil ifTrue:[^nil].	self register.	rwmode := false.	buffer1 := String new: 1.	self enableReadBuffering! !