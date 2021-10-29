'From Squeak4.2 of 4 February 2011 [latest update: #10966] on 10 April 2011 at 11:52:26 pm'!"Change Set:		MacClipboardInterpreterClassFix-MUDate:			10 April 2011Author:			Masashi UmezawaFixed JapaneseEnvironment class >> inputInterpreterClass for newer Mac VM (including Cog)"!!JapaneseEnvironment class methodsFor: 'subclass responsibilities' stamp: 'mu 4/10/2011 23:50'!clipboardInterpreterClass	| platformName osVersion |	platformName := SmalltalkImage current  platformName.	osVersion := SmalltalkImage current osVersion.	(platformName = 'Win32' and: [osVersion = 'CE']) 		ifTrue: [^NoConversionClipboardInterpreter].	platformName = 'Win32' ifTrue: [^UTF8ClipboardInterpreter].	platformName = 'Mac OS' 		ifTrue: [			^((((SmalltalkImage current vmVersion reverse upTo: Character space) reverse) upTo: $.) asInteger >= 4) ifTrue: [MacUTF8ClipboardInterpreter] ifFalse: [MacShiftJISClipboardInterpreter]].	^platformName = 'unix' 		ifTrue: 			[(ShiftJISTextConverter encodingNames includes: X11Encoding getEncoding) 				ifTrue: [MacShiftJISClipboardInterpreter]				ifFalse: [UnixJPClipboardInterpreter]]		ifFalse: [ UTF8ClipboardInterpreter ]! !