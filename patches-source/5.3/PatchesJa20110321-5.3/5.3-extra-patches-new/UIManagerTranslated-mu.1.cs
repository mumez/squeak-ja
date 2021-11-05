'From Squeak5.3 of 28 May 2021 [latest update: #19459] on 5 November 2021 at 11:25:35 am'!"Change Set:		UIManagerTranslated-muDate:			5 November 2021Author:			Masashi UmezawaInserted #translated message sends to UIManager methods"!!UIManager methodsFor: 'utilities' stamp: 'mu 11/5/2021 11:15'!askForProvidedAnswerTo: queryString ifSupplied: supplyBlock	^ (ProvideAnswerNotification signal: queryString asString translated) ifNotNil: supplyBlock! !!MorphicUIManager methodsFor: 'ui requests' stamp: 'mu 11/5/2021 11:17'!chooseDirectory: label from: dir	"Let the user choose a file matching the given patterns. Returns a file name."	self askForProvidedAnswerTo: label ifSupplied: [:answer | 		^ answer].			^ DirectoryChooserDialog openOn: dir label: label translated! !!MorphicUIManager methodsFor: 'ui requests' stamp: 'mu 11/5/2021 11:17'!chooseFileMatching: patterns label: aString	"Let the user choose a file matching the given patterns. Returns a file name."	self askForProvidedAnswerTo: aString ifSupplied: [:answer | 		^ answer].		^ FileChooserDialog openOnPattern: patterns label: aString translated! !!MorphicUIManager methodsFor: 'ui requests' stamp: 'mu 11/5/2021 11:17'!chooseFileMatchingSuffixes: suffixList label: aString	"Let the user choose a file matching the given suffixes. Returns a file name."	self askForProvidedAnswerTo: aString ifSupplied: [:answer | 		^ answer].		^ FileChooserDialog openOnSuffixList: suffixList label: aString translated	! !!MorphicUIManager methodsFor: 'ui requests' stamp: 'mu 11/5/2021 11:17'!chooseFont: titleString for: aModel setSelector: setSelector getSelector: getSelector	"Open a font-chooser for the given model"	self askForProvidedAnswerTo: titleString ifSupplied: [:answer | 		^ answer].		^FontChooserTool default		openWithWindowTitle: titleString translated		for: aModel 		setSelector: setSelector 		getSelector: getSelector! !!MorphicUIManager methodsFor: 'ui requests' stamp: 'mu 11/5/2021 11:17'!chooseFrom: aList lines: linesArray title: aString 	"Choose an item from the given list. Answer the index of the selected item. Cancel value is 0.		There are several (historical) reasons for building a button dialog instead of a list chooser for small lists:	1) Unfortunately, there is existing code that uses this call to create simple confirmation dialogs with a list of #(yes no cancel).	2) Unfortunately, there is existing code that uses this call to mimick a drop-down menu with a (compact) pop-up menu."	self askForProvidedAnswerTo: aString ifSupplied: [:answer | 		(answer = #cancel or: [answer isNil]) ifTrue: [^ 0].		^ aList indexOf: answer].		aList ifEmpty: [^ 0].	aList size <= 7 ifTrue: [		| dialog |		dialog := DialogWindow new			title: 'Please Choose' translated;			message: aString;			filterEnabled: true;			autoCancel: true; "Like a pop-up menu, click anywhere to dismiss."			yourself.		aList doWithIndex: [:ea :index |			dialog createButton: ea value: index].		dialog selectedButtonIndex: 1.		^ dialog getUserResponseAtHand ifNil: [0]].		^ ListChooser chooseFrom: aList title: aString translated! !!MorphicUIManager methodsFor: 'ui requests' stamp: 'mu 11/5/2021 11:18'!chooseFromOrAddTo: aList lines: linesArray title: aString	self askForProvidedAnswerTo: aString ifSupplied: [:answer | 		^ answer].	^ ListChooser		chooseItemFrom: aList		title: aString translated		addAllowed: true! !!MorphicUIManager methodsFor: 'ui requests' stamp: 'mu 11/5/2021 11:18'!chooseMultipleFrom: aList lines: linesArray title: aString	"Choose one or more items from the given list. Answer the indices of the selected items."		self askForProvidedAnswerTo: aString ifSupplied: [:answer | 		^ answer].		^ ListMultipleChooser		chooseFrom: aList		title: aString translated! !!MorphicUIManager methodsFor: 'ui requests' stamp: 'mu 11/5/2021 11:18'!chooseMultipleFrom: labelList values: valueList lines: linesArray title: aString	"Choose one or more items from the given list. Answer the selected items."	self askForProvidedAnswerTo: aString ifSupplied: [:answer | 		^ answer].	^ (ListMultipleChooser		chooseFrom: labelList		title: aString translated) ifNotNil: [:indexList |			indexList collect: [:index | valueList at: index]]! !!MorphicUIManager methodsFor: 'ui requests' stamp: 'mu 11/5/2021 11:15'!confirm: queryString	"Put up a yes/no menu with caption queryString. Answer true if the 	response is yes, false if no. This is a modal question--the user must 	respond yes or no."	self askForProvidedAnswerTo: queryString ifSupplied: [:answer | 		^ answer].		^ UserDialogBoxMorph confirm: queryString translated! !!MorphicUIManager methodsFor: 'ui requests' stamp: 'mu 11/5/2021 11:16'!confirm: aString orCancel: cancelBlock	"Put up a yes/no/cancel menu with caption aString. Answer true if  	the response is yes, false if no. If cancel is chosen, evaluate  	cancelBlock. This is a modal question--the user must respond yes or no."	self askForProvidedAnswerTo: aString ifSupplied: [:answer | 		^ (answer = #cancel or: [answer isNil]) 			ifTrue: [cancelBlock value]			ifFalse: [answer]].		^ UserDialogBoxMorph confirm: aString translated orCancel: cancelBlock! !!MorphicUIManager methodsFor: 'ui requests' stamp: 'mu 11/5/2021 11:18'!confirm: aString orCancel: cancelBlock title: titleString	"Put up a yes/no/cancel menu with caption aString, and titleString to label the dialog.	Answer true if  the response is yes, false if no. If cancel is chosen, evaluate cancelBlock.	This is a modal question--the user must respond yes or no."	self askForProvidedAnswerTo: aString ifSupplied: [:answer | 		^ (answer = #cancel or: [answer isNil]) 			ifTrue: [cancelBlock value]			ifFalse: [answer]].		^ UserDialogBoxMorph		confirm: aString translated		orCancel: cancelBlock		title: titleString translated		at: nil! !!MorphicUIManager methodsFor: 'ui requests' stamp: 'mu 11/5/2021 11:18'!confirm: queryString title: titleString	"Put up a yes/no menu with caption queryString, and titleString to label the dialog.	Answer true if the response is yes, false if no. This is a modal question--the user	must respond yes or no."	self askForProvidedAnswerTo: queryString ifSupplied: [:answer | 		^ answer].		^ UserDialogBoxMorph confirm: queryString translated title: titleString translated! !!MorphicUIManager methodsFor: 'ui requests' stamp: 'mu 11/5/2021 11:19'!confirm: queryString title: titleString trueChoice: trueChoice falseChoice: falseChoice	"Put up a yes/no menu with caption queryString, and titleString to label the dialog.	The actual wording for the two choices will be as provided in the trueChoice and	falseChoice parameters. Answer true if the response is the true-choice, false if it	is the false-choice. This is a modal question -- the user must respond one way or	the other."	self askForProvidedAnswerTo: queryString ifSupplied: [:answer | 		^ answer isBoolean 			ifTrue: [answer]			ifFalse: [trueChoice = answer]].		^ UserDialogBoxMorph confirm: queryString translated title: titleString translated trueChoice: trueChoice falseChoice: falseChoice ! !!MorphicUIManager methodsFor: 'ui requests' stamp: 'mu 11/5/2021 11:19'!confirm: queryString trueChoice: trueChoice falseChoice: falseChoice 	"Put up a yes/no menu with caption queryString. The actual wording for the two choices will be as provided in the trueChoice and falseChoice parameters. Answer true if the response is the true-choice, false if it's the false-choice.	This is a modal question -- the user must respond one way or the other."	self askForProvidedAnswerTo: queryString ifSupplied: [:answer | 		^ answer].		^ UserDialogBoxMorph confirm: queryString translated trueChoice: trueChoice falseChoice: falseChoice ! !!MorphicUIManager methodsFor: 'ui requests' stamp: 'mu 11/5/2021 11:19'!displayProgress: titleString at: aPoint from: minVal to: maxVal during: workBlock 	"Display titleString as a caption over a progress bar while workBlock is evaluated."	| result progress |	progress := SystemProgressMorph		position: aPoint		label: titleString translated		min: minVal		max: maxVal.	[ [ result := workBlock value: progress ]		on: ProgressNotification		do:			[ : ex | ex extraParam isString ifTrue:				[ SystemProgressMorph uniqueInstance					labelAt: progress					put: ex extraParam ].			ex resume ] ] ensure: [ SystemProgressMorph close: progress ].	^ result! !!MorphicUIManager methodsFor: 'ui requests' stamp: 'mu 11/5/2021 11:19'!edit: aText label: labelString shouldStyle: aBoolean accept: anAction	"Open an editor on the given string/text"	| window |	window := Workspace open.	labelString ifNotNil: [ window setLabel: labelString translated ].	window model		shouldStyle: aBoolean;		acceptContents:  aText;		acceptAction: anAction.	^ window! !!MorphicUIManager methodsFor: 'ui requests' stamp: 'mu 11/5/2021 11:19'!inform: aString	"Display a message for the user to read and then dismiss"	self askForProvidedAnswerTo: aString ifSupplied: [:answer | 		^ answer].		^ UserDialogBoxMorph inform: aString translated! !!MorphicUIManager methodsFor: 'ui requests' stamp: 'mu 11/5/2021 11:20'!multiLineRequest: queryString centerAt: aPoint initialAnswer: defaultAnswer answerHeight: answerHeight	"Create a multi-line instance of me whose question is queryString with	the given initial answer. Invoke it centered at the given point, and	answer the string the user accepts.  Answer nil if the user cancels.  An	empty string returned means that the ussr cleared the editing area and	then hit 'accept'.  Because multiple lines are invited, we ask that the user	use the ENTER key, or (in morphic anyway) hit the 'accept' button, to 	submit; that way, the return key can be typed to move to the next line."	self askForProvidedAnswerTo: queryString ifSupplied: [:answer | 		^ answer = #default				ifTrue: [defaultAnswer]				ifFalse: [answer]].		^ FillInTheBlankMorph 		request: queryString translated		initialAnswer: defaultAnswer translated		centerAt: aPoint 		inWorld: self currentWorld		onCancelReturn: nil		acceptOnCR: false! !!MorphicUIManager methodsFor: 'ui requests' stamp: 'mu 11/5/2021 11:23'!request: queryString initialAnswer: defaultAnswer centerAt: aPoint 	"Create an instance of me whose question is queryString with the given	initial answer. Invoke it centered at the given point, and answer the	string the user accepts. Answer the empty string if the user cancels."	self askForProvidedAnswerTo: queryString ifSupplied: [:answer | 		^ answer = #default				ifTrue: [defaultAnswer translated]				ifFalse: [answer]].		^ FillInTheBlankMorph request: queryString translated initialAnswer: defaultAnswer translated centerAt: aPoint! !!MorphicUIManager methodsFor: 'ui requests' stamp: 'mu 11/5/2021 11:20'!requestPassword: queryString	"Create an instance of me whose question is queryString. Invoke it centered	at the cursor, and answer the string the user accepts. Answer the empty 	string if the user cancels."	self askForProvidedAnswerTo: queryString ifSupplied: [:answer | 		^ answer].				^ FillInTheBlankMorph requestPassword: queryString translated! !!MorphicUIManager methodsFor: 'ui requests' stamp: 'mu 11/5/2021 11:23'!request: queryString initialAnswer: defaultAnswer 	"Create an instance of me whose question is queryString with the given 	initial answer. Invoke it centered at the given point, and answer the 	string the user accepts. Answer the empty string if the user cancels."	self askForProvidedAnswerTo: queryString ifSupplied: [:answer | 		^ answer = #default				ifTrue: [defaultAnswer translated]				ifFalse: [answer]].		^FillInTheBlankMorph request: queryString translated initialAnswer: defaultAnswer translated! !!MorphicUIManager methodsFor: 'ui requests' stamp: 'mu 11/5/2021 11:23'!saveFilenameRequest: queryString initialAnswer: defaultAnswer 	"Open a FileSaverDialog to ask for a place and filename to use for saving a file. The initial suggestion for the filename is defaultAnswer but the user may choose any existing file or type in a new name entirely"	self askForProvidedAnswerTo: queryString ifSupplied: [:answer | 		^ answer = #default				ifTrue: [defaultAnswer translated]				ifFalse: [answer]].	^ FileSaverDialog openOnInitialFilename: defaultAnswer translated label: queryString translated! !!MorphicUIManager methodsFor: 'ui project indirecting' stamp: 'mu 11/5/2021 11:20'!openPluggableFileList: aPluggableFileList label: aString in: aWorld	"PluggableFileList is being deprecated and this can go away soon"	^aPluggableFileList morphicOpenLabel: aString translated in: aWorld! !