'From Squeak5.3 of 28 May 2021 [latest update: #19459] on 11 November 2021 at 10:05:23 pm'!"Change Set:		DialogTitleMessageTranslated-mu
Date:			11 November 2021
Author:			Masashi Umezawa

Added #translated to Dialog title & message"!!DialogWindow methodsFor: 'accessing' stamp: 'mu 11/11/2021 22:03'!message: aStringOrText	messageMorph contents: aStringOrText translated.	self setMessageParameters.! !!DialogWindow methodsFor: 'accessing' stamp: 'mu 11/11/2021 22:03'!title: aString	titleMorph contents: aString asText translated.	self setTitleParameters.! !