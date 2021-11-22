'From Squeak5.3 of 28 May 2021 [latest update: #19459] on 22 November 2021 at 10:29:04 pm'!"Change Set:		ChangeFontSizeFix-muDate:			22 November 2021Author:			Masashi UmezawaAvoid using bitmap font for displaying multibyte characters"!!Preferences class methodsFor: 'prefs - fonts' stamp: 'mu 11/22/2021 22:19'!changeFontSize: delta	| theme base fonts |	delta = 0 ifTrue: [self restoreDefaultFonts].	(UserInterfaceTheme current name beginsWith: 'Demo')		ifFalse: [			"Create DEMO version of current theme."			theme := UserInterfaceTheme named: 'Demo'.			theme merge: UserInterfaceTheme current overwrite: true.			theme apply].	base := (TextStyle defaultFont name beginsWith: 'Darkmap')		ifTrue: ['Darkmap DejaVu Sans'] ifFalse: ['BitstreamVeraSans'].	fonts := {		{#SystemFont. base}.		{#FixedFont. 'BitstreamVeraSansMono'}.		{#ListFont. base}.		{#FlapFont. base}.		{#EToysFont. base}.		{#PaintBoxButtonFont. base}.		{#MenuFont. base}.		{#WindowTitleFont. base, ' B'}.		{#BalloonHelpFont. base}.		{#CodeFont. base}.		{#ButtonFont. base}.	} collect: [:ary || newPtSize |		newPtSize := (self perform: ('standard', ary first) asSymbol) pointSize + delta.		{('set', ary first, 'To:') asSymbol. ary second. newPtSize}	].	self setDefaultFonts: fonts.! !