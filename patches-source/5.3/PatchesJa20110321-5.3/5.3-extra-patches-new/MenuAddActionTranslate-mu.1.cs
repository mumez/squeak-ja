'From Squeak5.3 of 28 May 2021 [latest update: #19459] on 6 November 2021 at 10:34:56 pm'!!CustomMenu methodsFor: 'construction' stamp: 'mu 4/25/2010 00:13'!add: aString action: actionItem
	"Add the given string as the next menu item. If it is selected, the given action (usually but not necessarily a symbol) will be returned to the client."

	| s |
	aString ifNil: [^ self addLine].
	s := String new: aString translated size + 2.
	s at: 1 put: Character space.
	s replaceFrom: 2 to: s size - 1 with: aString.
	s at: s size put: Character space.
	labels addLast: s.
	selections addLast: actionItem.! !!MenuMorph methodsFor: 'construction' stamp: 'mu 11/6/2021 22:34'!add: aString action: aSymbolOrValuable 	"Append a menu item with the given label. If the item is selected, it will send the given selector to the default target object."	"Details: Note that the menu item added captures the default target object at the time the item is added; the default target can later be changed before added additional items without affecting the targets of previously added entries. The model is that each entry is like a button that knows everything it needs to perform its action."	aSymbolOrValuable isSymbol		ifTrue:			[ self				add: aString translated				target: defaultTarget				selector: aSymbolOrValuable				argumentList: Array empty ]		ifFalse:			[ self				add: aString translated				target: aSymbolOrValuable				selector: #value				argumentList: Array empty ]! !!PluggableMenuSpec methodsFor: 'construction' stamp: 'mu 4/25/2010 00:13'!add: aString action: aMessageSend
	| item |
	item := self addMenuItem.
	item label: aString translated.
	item action: aMessageSend.
	^item! !