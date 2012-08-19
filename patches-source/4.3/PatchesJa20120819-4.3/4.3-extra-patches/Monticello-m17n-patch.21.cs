'From Squeak4.3 of 22 December 2011 [latest update: #11860] on 19 August 2012 at 4:22:38 pm'!"Change Set:		Monticello-m17n-patchDate:			19 August 2012Author:			Masashi UmezawaFix: Avoid using WideString if not necessary."!!MCMczReader methodsFor: 'as yet unclassified' stamp: 'mu 2/2/2008 14:23'!parseMember: fileName	| readStream toReadString tokens |	readStream := MultiByteBinaryOrTextStream with: (self zip contentsOf: fileName).	readStream reset.	readStream setConverterForCode.	toReadString := readStream contents.	tokens := (self scanner scanTokens: toReadString) first.	^ self associate: tokens! !!MCMczWriter methodsFor: 'visiting' stamp: 'mu 2/2/2008 14:38'!writePackage: aPackage	self addInfoString: (self serializePackage: aPackage) at: 'package'! !!MCMczWriter methodsFor: 'visiting' stamp: 'mu 10/29/2007 22:30'!writeSnapshot: aSnapshot	self addSourceString: (self serializeDefinitions: aSnapshot definitions) at: 'snapshot/source.', self snapshotWriterClass extension.	self addString: (self serializeInBinary: aSnapshot) at: 'snapshot.bin'! !!MCMczWriter methodsFor: 'visiting' stamp: 'mu 2/2/2008 14:38'!writeVersionDependency: aVersionDependency	| string |	string := (self serializeVersionInfo: aVersionDependency versionInfo).	self addInfoString: string at: 'dependencies/', aVersionDependency package name! !!MCMczWriter methodsFor: 'visiting' stamp: 'mu 2/2/2008 14:38'!writeVersionInfo: aVersionInfo	| string |	string := self serializeVersionInfo: aVersionInfo.	self addInfoString: string at: 'version'.! !!MCMczWriter methodsFor: 'writing' stamp: 'mu 2/2/2008 14:38'!addInfoString: string at: path	self addString: (self infoStringFrom: string) at: path	! !!MCMczWriter methodsFor: 'writing' stamp: 'mu 10/29/2007 22:30'!addSourceString: string at: path	self addString: (self sourceStringFrom: string) at: path	! !!MCMczWriter methodsFor: 'private' stamp: 'mu 8/19/2012 16:12'!infoStringFrom: rawString 	"create mac-roman or utf-8 with BOM -- for backward compatibility"	| responseStream |	rawString isByteString ifTrue: [^rawString]. "mac-roman"	responseStream := MultiByteBinaryOrTextStream on: WideString new encoding: 'utf-8'.	UTF8TextConverter writeBOMOn: responseStream.	responseStream nextPutAll: rawString.	responseStream binary.	responseStream reset.	^ responseStream contents! !!MCMczWriter methodsFor: 'private' stamp: 'mu 8/19/2012 15:30'!sourceStringFrom: rawString 	"create utf-8 with BOM string -- which is default source file format in Squeak"	|  responseStream |	rawString isByteString ifTrue: [^rawString].	responseStream := MultiByteBinaryOrTextStream on: WideString new encoding: 'utf-8'.	UTF8TextConverter writeBOMOn: responseStream.	responseStream nextPutAll: rawString.	responseStream binary.	responseStream reset.	^ responseStream contents! !!MCMcdWriter methodsFor: 'as yet unclassified' stamp: 'mu 2/2/2008 15:02'!writeBaseInfo: aVersionInfo	| string |	string := self serializeVersionInfo: aVersionInfo.	self addInfoString: string at: 'base'.! !!MCMcdWriter methodsFor: 'as yet unclassified' stamp: 'mu 10/29/2007 22:30'!writeNewDefinitions: aCollection	self addSourceString: (self serializeDefinitions: aCollection) at: 'new/source.', self snapshotWriterClass extension.! !!MCMcdWriter methodsFor: 'as yet unclassified' stamp: 'mu 10/29/2007 22:30'!writeOldDefinitions: aCollection	self addSourceString: (self serializeDefinitions: aCollection) at: 'old/source.', self snapshotWriterClass extension.! !!MCStReader methodsFor: 'evaluating' stamp: 'mu 1/9/2006 19:22'!readStream	^ ('!!!!', stream contents convertFromWithConverter: UTF8TextConverter new) readStream! !!MczInstaller methodsFor: 'utilities' stamp: 'mu 2/2/2008 14:48'!parseMember: fileName	| readStream toReadString tokens |	readStream := MultiByteBinaryOrTextStream with: (zip contentsOf: fileName).	readStream reset.	readStream setConverterForCode.	toReadString := readStream contents.	tokens := (self scanner scanTokens: toReadString) first.	^ self associate: tokens! !