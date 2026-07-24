'From Squeak6.0 of 8 June 2026 [latest update: #22156] on 24 July 2026 at 11:00:37 am'!
"Change Set:		InternalTranslator-loadFromStream-Fix
Date:			24 July 2026
Author:			Masashi Umezawa

Fixed InternalTranslator >> loadFromFileIn: to load translation data properly from .translation file"!


!InternalTranslator methodsFor: 'private store-retrieve' stamp: 'MU 7/24/2026 10:57'!
loadFromFileIn: tempTranslator 
	"Load translations from a tempTranslator"
	self mergeTranslations: tempTranslator generics.
	tempTranslator generics keysDo: [:each | self class registerPhrase: each].! !

!InternalTranslator methodsFor: 'private store-retrieve' stamp: 'MU 7/24/2026 10:55'!
loadFromStream: stream 
	"Load translations from an external file"
	| header isFileIn |
	header := '''Translation dictionary'''.
	isFileIn := (stream next: header size)
				= header.
	stream reset.
	isFileIn
		ifTrue: [self loadFromFileIn: (stream fileInAnnouncing: 'Loading ' translated, stream localName)]
		ifFalse: [self loadFromRefStream: stream].! !

