'From Squeak5.3 of 28 May 2021 [latest update: #19459] on 7 November 2021 at 10:13:33 pm'!!CustomMenu methodsFor: 'construction' stamp: 'mu 4/25/2010 00:13'!add: aString action: actionItem
	"Add the given string as the next menu item. If it is selected, the given action (usually but not necessarily a symbol) will be returned to the client."

	| s |
	aString ifNil: [^ self addLine].
	s := String new: aString translated size + 2.
	s at: 1 put: Character space.
	s replaceFrom: 2 to: s size - 1 with: aString.
	s at: s size put: Character space.
	labels addLast: s.
	selections addLast: actionItem.! !!MenuMorph methodsFor: 'construction' stamp: 'mu 11/7/2021 22:12'!add: aString target: target selector: aSymbol argumentList: argList	"Append a menu item with the given label. If the item is selected, it will send the given selector to the target object with the given arguments. If the selector takes one more argument than the number of arguments in the given list, then the triggering event is supplied as as the last argument."	| item |	item := MenuItemMorph new		contents: aString translated;		target: target;		selector: aSymbol;		arguments: argList asArray.	self addMorphBack: item.! !!PluggableMenuSpec methodsFor: 'construction' stamp: 'mu 4/25/2010 00:13'!add: aString action: aMessageSend
	| item |
	item := self addMenuItem.
	item label: aString translated.
	item action: aMessageSend.
	^item! !