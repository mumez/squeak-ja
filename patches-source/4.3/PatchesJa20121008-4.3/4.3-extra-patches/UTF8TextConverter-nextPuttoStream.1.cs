'From Squeak4.3 of 22 December 2011 [latest update: #11860] on 7 October 2012 at 11:14:52 pm'!"Change Set:		UTF8TextConverter-nextPuttoStreamDate:			7 October 2012Author:			Masashi UmezawaReverted UTF8TextConverter>>nextPut:toStream: to 4.2 version.4.3 version crashes VM in worst cases."!!UTF8TextConverter methodsFor: 'conversion' stamp: 'ar 4/9/2005 22:29'!nextPut: aCharacter toStream: aStream 	| leadingChar nBytes mask shift ucs2code |	aStream isBinary ifTrue: [^aCharacter storeBinaryOn: aStream].	leadingChar := aCharacter leadingChar.	(leadingChar = 0 and: [aCharacter asciiValue < 128]) ifTrue: [		aStream basicNextPut: aCharacter.		^ aStream.	].	"leadingChar > 3 ifTrue: [^ aStream]."	ucs2code := aCharacter asUnicode.	ucs2code ifNil: [^ aStream].	nBytes := ucs2code highBit + 3 // 5.	mask := #(128 192 224 240 248 252 254 255) at: nBytes.	shift := nBytes - 1 * -6.	aStream basicNextPut: (Character value: (ucs2code bitShift: shift) + mask).	2 to: nBytes do: [:i | 		shift := shift + 6.		aStream basicNextPut: (Character value: ((ucs2code bitShift: shift) bitAnd: 63) + 128).	].	^ aStream.! !