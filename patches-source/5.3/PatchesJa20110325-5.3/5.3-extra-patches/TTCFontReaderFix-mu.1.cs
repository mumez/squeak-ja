'From Squeak5.3 of 28 May 2021 [latest update: #19459] on 2 December 2021 at 4:18:55 pm'!"Change Set:		TTCFontReaderFix-muDate:			2 December 2021Author:			Masashi Umezawa-A quick fix for avoiding nil cmap in processCharacterMappingTable:-Salvaged #decodeCmapFmtTable: from Squeak4.4"!!TTCFontReader methodsFor: 'private' stamp: 'mu 12/2/2021 16:12'!decodeCmapFmtTable: entry	| cmapFmt length entryCount segCount segments offset cmap firstCode |	cmapFmt := entry nextUShort.	length := entry nextUShort.	entry skip: 2. "skip version"	cmapFmt = 0 ifTrue: "byte encoded table"		[length := length - 6. 		"should be always 256"		length <= 0 ifTrue: [^ nil].	"but sometimes, this table is empty"		cmap := Array new: length.		entry nextBytes: length into: cmap startingAt: entry offset.		^ cmap].	cmapFmt = 4 ifTrue: "segment mapping to deltavalues"		[segCount := entry nextUShort // 2.		entry skip: 6. "skip searchRange, entrySelector, rangeShift"		segments := Array new: segCount.		segments := (1 to: segCount) collect: [:e | Array new: 4].		1 to: segCount do: [:i | (segments at: i) at: 2 put: entry nextUShort]. "endCount"		entry skip: 2. "skip reservedPad"		1 to: segCount do: [:i | (segments at: i) at: 1 put: entry nextUShort]. "startCount"		1 to: segCount do: [:i | (segments at: i) at: 3 put: entry nextShort]. "idDelta"		offset := entry offset.		1 to: segCount do: [:i | (segments at: i) at: 4 put: entry nextUShort]. "idRangeOffset"		cmap := Array new: 65536 withAll: 0.		segments withIndexDo:			[:seg :si | | code |			seg first to: seg second do:				[:i |					seg last > 0 ifTrue:						["offset to glypthIdArray - this is really C-magic!!"						entry offset: i - seg first - 1 * 2 + seg last + si + si + offset.						code := entry nextUShort.						code > 0 ifTrue: [code := code + seg third]]					ifFalse:						["simple offset"						code := i + seg third].					cmap at: i + 1 put: (code \\ 16r10000)]].		^ cmap].	cmapFmt = 6 ifTrue: "trimmed table"		[firstCode := entry nextUShort.		entryCount := entry nextUShort.		cmap := Array new: entryCount + firstCode withAll: 0.		entryCount timesRepeat:			[cmap at: (firstCode := firstCode + 1) put: entry nextUShort].		^ cmap].	^ nil! !!TTCFontReader methodsFor: 'processing' stamp: 'mu 12/2/2021 16:12'!processCharacterMappingTable: entry	"Read the font's character to glyph index mapping table.	If an appropriate mapping can be found then return an association	with the format identifier and the contents of the table"	|  initialOffset nSubTables assoc |	initialOffset := entry offset.	entry skip: 2. "Skip table version"	nSubTables := entry nextUShort.	assoc := nil.	1 to: nSubTables do:[:i| | copy pID sID offset cmap |		pID := entry nextUShort.		sID := entry nextUShort.		offset := entry nextULong.		"Check if this is either a Macintosh encoded table		or a Windows encoded table"		cmap := nil.		(pID = 1 or:[pID = 3]) ifTrue:[ |nextCmap |			"Go to the beginning of the table"			copy := entry copy.			copy offset: initialOffset + offset.			nextCmap := self decodeCmapFmtTable: copy.			"(pID = 1 and: [cmap notNil])" "Prefer Macintosh encoding over everything else"				"ifTrue: [pID -> cmap]."			(assoc notNil and: [nextCmap isNil]) ifTrue: [^ pID -> cmap].			assoc := pID -> (cmap := nextCmap). "Keep it in case we don't find a Mac encoded table"		].	].	^assoc! !