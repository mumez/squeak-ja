'From Squeak5.3 of 28 May 2021 [latest update: #19459] on 7 November 2021 at 10:13:33 pm'!
	"Add the given string as the next menu item. If it is selected, the given action (usually but not necessarily a symbol) will be returned to the client."

	| s |
	aString ifNil: [^ self addLine].
	s := String new: aString translated size + 2.
	s at: 1 put: Character space.
	s replaceFrom: 2 to: s size - 1 with: aString.
	s at: s size put: Character space.
	labels addLast: s.
	selections addLast: actionItem.! !
	| item |
	item := self addMenuItem.
	item label: aString translated.
	item action: aMessageSend.
	^item! !