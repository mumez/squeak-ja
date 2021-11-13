'From Squeak5.3 of 28 May 2021 [latest update: #19459] on 1 November 2021 at 10:34:33 pm'!
"Change Set:		MonticelloTranslated53-mu
Date:			01 November 2021
Author:			Masashi Umezawa

More #translated fixes for Monticello bundled in Squeak 5.3 "!


!MCTool methodsFor: 'morphic ui' stamp: 'mu 7/13/2013 22:47'!
fillMenu: aMenu fromSpecs: anArray
	anArray do:
		[:spec |
		spec == #addLine
			ifTrue: [aMenu addLine]
			ifFalse:
				[aMenu
					add: spec first translated
					target: self
					selector: spec second
					argumentList: (spec allButFirst: 2)]].
	^aMenu! !


!MCWorkingCopyBrowser methodsFor: 'morphic ui' stamp: 'mu 11/1/2021 22:33'!
repositoryListMenu: aMenu
	"first add repository-specific commands"
	self repository ifNotNil:
		[ self
			fillMenu: aMenu
			fromSpecs:
				#(('open repository' #openRepository)
				('edit repository info' #editRepository)
				('add to package...' #addRepositoryToPackage)
				('remove repository' #removeRepository)
				('demote to bottom' #demoteRepository)
				('copy image versions here' #copyImageVersions)).
		aMenu
			add:
				(self repository alwaysStoreDiffs
					ifTrue: ['store full versions']
					ifFalse: ['store diffs']) translated
				target: self
				selector: #toggleDiffs ;
			addLine ].
	"then the non-specific commands"
	^self fillMenu: aMenu fromSpecs:
		#(	('load repositories' #loadRepositories)
		 	('save repositories' #saveRepositories)
			('flush cached versions' #flushCachedVersions))! !

