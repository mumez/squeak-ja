'From Squeak5.3 of 28 May 2021 [latest update: #19459] on 24 November 2021 at 11:59:13 pm'!
"Change Set:		X11EncodingFallbackToUtf8-mu
Date:			24 November 2021
Author:			Masashi Umezawa

X11Encoding class >> encoding returns 'utf-8' as a fallback encoding when primitive call is  failed."!

!X11Encoding class methodsFor: 'encoding' stamp: 'mu 11/24/2021 23:56'!
encoding

	| enc |
	enc := self getEncoding.
	enc isString ifTrue: [ ^enc asLowercase].
	^'utf-8'! !

