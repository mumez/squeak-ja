'From Squeak4.1 of 17 April 2010 [latest update: #9957] on 30 April 2010 at 11:45:34 pm'!"Change Set:		IsFontAvailableFix-muDate:			30 April 2010Author:			Masashi UmezawaIf TextStyle defaultFont is a TTCFont, probably we should ignore the check."!!LanguageEnvironment methodsFor: 'fonts support' stamp: 'mu 4/30/2010 23:43'!isFontAvailable	| encoding f |	encoding := self leadingChar + 1.	f := TextStyle defaultFont.	f isTTCFont ifTrue: [^true].	f isFontSet ifTrue: [		f fontArray			at: encoding			ifAbsent: [^ false].		^ true	].	encoding = 1 ifTrue: [^ true].	f fallbackFont isFontSet ifFalse: [^false].	f fallbackFont fontArray		at: encoding		ifAbsent: [^ false].	^ true! !