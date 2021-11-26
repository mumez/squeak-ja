'From Squeak5.3 of 28 May 2021 [latest update: #19459] on 13 November 2021 at 12:03:50 am'!
"Change Set:		JapaneseEnvironmentConverterClassFix-mu
Date:			5 November 2021
Author:			Masashi Umezawa

Fixed converter logic for VM using UTF8"!


!JapaneseEnvironment class methodsFor: 'subclass responsibilities' stamp: 'mu 11/13/2021 00:02'!
clipboardInterpreterClass
	| platformName osVersion |
	platformName := Smalltalk platformName.
	osVersion := Smalltalk osVersion.
	(platformName = 'Win32' and: [osVersion = 'CE']) 
		ifTrue: [^NoConversionClipboardInterpreter].
	platformName = 'Win32' ifTrue: [^UTF8ClipboardInterpreter].
	platformName = 'Mac OS' 
		ifTrue: [ | vmVersion |
			vmVersion := (MacUnicodeInputInterpreter new majorMinorBuildFrom: Smalltalk vmVersion) first.
			^(vmVersion asInteger >= 4) ifTrue: [MacUTF8ClipboardInterpreter] ifFalse: [MacShiftJISClipboardInterpreter]].
	^platformName = 'unix' 
		ifTrue: 
			[(ShiftJISTextConverter encodingNames includes: X11Encoding encoding) 
				ifTrue: [MacShiftJISClipboardInterpreter]
				ifFalse: [UnixJPClipboardInterpreter]]
		ifFalse: [ UTF8ClipboardInterpreter ]! !

!JapaneseEnvironment class methodsFor: 'subclass responsibilities' stamp: 'mu 11/5/2021 12:09'!
systemConverterClass
	| platformName osVersion encoding |
	platformName := Smalltalk platformName.
	osVersion := Smalltalk osVersion.
	(platformName = 'Win32') 
		ifTrue: [^UTF8TextConverter].
	(platformName = 'ZaurusOS') 
		ifTrue: [^ShiftJISTextConverter].
	platformName = 'Mac OS' 
		ifTrue: 
			[^((osVersion indexOf: $.) > 4 "i.e. not 9xx.n, but 10xx.n, 11xx.n etc" ) 
				ifTrue: [UTF8TextConverter]
				ifFalse: [ShiftJISTextConverter]].
	platformName = 'unix' 
		ifTrue: 
			[encoding := X11Encoding encoding.
			encoding ifNil: [^UTF8TextConverter].
			(encoding = 'utf-8') 
				ifTrue: [^UTF8TextConverter].				
			(encoding = 'shiftjis' or: [ encoding = 'sjis' ]) 
				ifTrue: [^ShiftJISTextConverter].				
			^EUCJPTextConverter].
	^UTF8TextConverter! !

