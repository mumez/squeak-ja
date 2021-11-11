translator := NaturalLanguageTranslator current. 

FileStream fileNamed: 'trans.tsv' do: [:str | 
	[str atEnd] whileFalse: [ | line tokens |
		line := str nextLine.
		tokens := line findTokens: String tab.
		(tokens size >= 2) ifTrue: [
			translator phrase: tokens first translation: tokens second 
		]
	]
].
