'From Squeak4.2 of 4 February 2011 [latest update: #10966] on 29 March 2011 at 10:44 pm'!
"Change Set:		AddMakeProjectLinkImpl-mu
Date:			29 March 2011
Author:			Masashi Umezawa

Added missing makeProjectLink implementations"!


!PluggableTextMorph methodsFor: 'menu commands' stamp: 'mu 3/29/2011 22:38'!
makeProjectLink
	self handleEdit: [textMorph editor makeProjectLink]! !


!SmalltalkEditor methodsFor: 'menu messages' stamp: 'mu 3/29/2011 22:41'!
makeProjectLink
	
	| attribute thisSel |
	
	thisSel := self selection.

	attribute := TextSqkProjectLink new. 
	thisSel := attribute analyze: self selection asString.

	thisSel ifNil: [^ true].
	self replaceSelectionWith: (thisSel asText addAttribute: attribute).
	^ true! !


!SmalltalkEditor reorganize!
('editing keys' changeEmphasis: emphasisExtras handleEmphasisExtra:with: invokePrettyPrint:)
('compatibility' select)
('do-its' tallyIt tallySelection)
('accessing' styler styler:)
('private' methodArgument: typeMethodArgument:)
('parenblinking' blinkPrevParen:)
('menu messages' makeProjectLink)
!

