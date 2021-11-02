'From Squeak3.9 of 7 November 2006 [latest update: #7067] on 27 January 2008 at 6:00:06 pm'!!ProcessBrowser methodsFor: 'process list' stamp: 'mu 1/27/2008 17:54'!processListMenu: menu 	| pw |	selectedProcess		ifNotNil: [| nameAndRules | 			nameAndRules := self nameAndRulesForSelectedProcess.			menu addList: {{'inspect (i)'. #inspectProcess}. {'explore (I)'. #exploreProcess}. {'inspect Pointers (P)'. #inspectPointers}}.	(Smalltalk includesKey: #PointerFinder)		ifTrue: [ menu add: 'chase pointers (c)' translated action: #chasePointers.  ].			nameAndRules second				ifTrue: [menu add: 'terminate (t)' translated action: #terminateProcess.					selectedProcess isSuspended						ifTrue: [menu add: 'resume (r)' translated action: #resumeProcess]						ifFalse: [menu add: 'suspend (s)' translated action: #suspendProcess]].			nameAndRules third				ifTrue: [menu addList: {{'change priority (p)'. #changePriority}. {'debug (d)'. #debugProcess}}].			menu addList: {{'profile messages (m)'. #messageTally}}.			(selectedProcess suspendingList isKindOf: Semaphore)				ifTrue: [menu add: 'signal Semaphore (S)' translated action: #signalSemaphore].			menu add: 'full stack (k)' translated action: #moreStack.			menu addLine].	menu addList: {{'find context... (f)'. #findContext}. {'find again (g)'. #nextContext}}.	menu addLine.	menu		add: (self isAutoUpdating				ifTrue: ['turn off auto-update (a)']				ifFalse: ['turn on auto-update (a)']) translated		action: #toggleAutoUpdate.	menu add: 'update list (u)' translated action: #updateProcessList.	pw := Smalltalk at: #CPUWatcher ifAbsent: [].	pw ifNotNil: [		menu addLine.		pw isMonitoring				ifTrue: [ menu add: 'stop CPUWatcher' translated action: #stopCPUWatcher ]				ifFalse: [ menu add: 'start CPUWatcher' translated action: #startCPUWatcher  ]	].	^ menu! !