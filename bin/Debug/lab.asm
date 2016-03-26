; Лабораторная работа №5
; Выполнил студент Михолап Роман группы ИВБ-2-14, вариант 15.
; Определить сумму отрицательных элементов массива из 9 целочисленных констант. 

lab5:	start	0
	import	input
;============== вывод подсказки ==============================
begin:	ldb	x, #text1
	ldb	r6, #text1len
	call	OutString	
;=============================================================			
	clear 	a	;подготовка цикла
	clear	r0
search:	+call	input	;вызов подпрограммы ввода целого числа. Оно сохранится в регистр A
	comp	x, #0	;Количество введенных цифр сохранится в регистре X
	jeq	search	;Если было введено 0 цифр, идем вводить число заново
	comp	a, #0	;сравниваем значение регистра A с 0.
	jeq	continue	
	jgt	continue	
	
	addr	r0, a	;суммируем отрицательные

continue:	ld	a, #32
	wd	#0	;вывели пробел
	compr	r10, a	;после вызова input в R10 запишется код символа, которым был завершен ввод - Пробел (32) или Enter (13)
	jeq	search	;если был введен пробел, продолжим поиск. Иначе завершим поиск.	
;============ Обработка полученного результата ===============	
next:	comp	r0, #0	
	jeq	NoResult
	jmp 	OutResult
;===============  Выводим "Нет результата!" ==================
NoResult:	ldb	x, #no
	ldb	r6, #nolen
	call	OutString
	jmp	Exit
;=============== Вывод результата ============================
OutResult:	clear	a
	subr	a, r0	;результат в r0
	mov	r0, a
;======= вывод "Сумма отрицательных элементов = " =============
	ldb	x, #text2
	ldb	r6, #text2len
	call	OutString
;==============================================================	
	ldb	a, #45	;вывод символа минус
	wd	#0
	mov 	a, r0	
	call	elementout
	jmp	Exit
;============= вывод строки ==================================
OutString:	addr	r6, x
onemore:	ldb	a, 0[x]
	wd	#0
	incr	x, r6
	jlt	onemore
	ret
;======= вывод числа =========================================
elementout:	ld	r3, #-99	
	push	r3		
	mov	r2, a	
positive:	ld	r3, #10	
	divr	r2, r3	
	mulr	r3, r2	
	subr	a, r3	
	add	a, #48	
	push	a	
	comp	r2, #0	
	jeq	Print	 
	mov	a, r2	
	jmp	positive	
Print:	pop	a	
	comp	a, #-99
	jeq	Exit	
	wd	#0	
	jmp	Print			
;========== зацикливание программы ===========================
Exit:	ld	x, #repeat	
	ld	r6, #repeatlen	
	call	OutString
	rd	#1
	wd	#0
	compb	a, #113	
	jeq	thatsall
	jmp	begin
;==============================================================
thatsall:	ret	
;================== Данные ====================================
repeat:	byte	10, 13, 'Нажмите q для окончания ввода', 10, 13
repeatlen:	equ	*-repeat
minuss:	byte	'-'	
text2:	byte	10, 13, 'Сумма отрицательных элементов: '
text2len:	equ	*-text2
text1:	byte	'Введите пару чисел. Программа определит сумму отрицательных. Конец ввода - Enter', 10, 13
text1len:	equ	*-text1
no:	byte	10, 13, 'Нет отрицательных элементов'
nolen:	equ	*-no

;======== Ввод с клавиатуры ============		
;Введенное число сохраняется в регистр A, код последнего введенного символа (пробел или enter) 
; возвращается в регистре R10, количество введенных цифр возвращается в регистре X
;Будут изменены значения следующих регистров: A, X, R10, R0, R9, R8. R0 будет сброшен в 0.
input:	csect	0
	clear	x	;число введенных цифр
	clear	r8	;здесь будет храниться само вводимое число
	clear	r9	;флаг отрицательного числа. r9==0 => >0; r9==1 => <0
	clear	a
read:	rd	#1	;прочли символ с клавиатуры
	comp	a, #Space
	jeq	SpOrEnt
	comp	a, #Enter
	jeq	SpOrEnt	;пробел и ENTER завершают ввод числа 
	comp	a, #Minus
	jeq	min

	comp	a, #47	;47 - код символа перед '0'; 58 - код символа после '9'
	jgt	checkNine
	jmp	read	;проверка на ввод цифры
checkNine:	comp	a, #58	
	jlt	Number	
	jmp	read
		

Number:	+comp	r8, #limit	; Обработчик цифр
	jgt	read	
	jeq	checkWord	;защита от переполнения 
addd:	wd	#0	;вывод введенной цифры в консоль
	sub	a, #48	
	add	x, #1	;увеличили количество введенных цифр
	mul	r8, #10	
	addr	r8, a	
	jmp	read
checkWord:	comp	a, #55	;защита от переполнения
	jgt	read
	jmp	addd	

min:	comp	r9, #0	; Обработчик минуса
	jgt	read	;проверка на корректность введенного минуса
	comp	r8, #0
	jgt	read	
	wd	#0	;вывод минуса
	ld	r9, #1	
	jmp	read	


SpOrEnt:	mov	r10, a	; Обработчик кода Пробела и Enter
	comp	a, #Enter
	jeq	enterr
enterOK:	comp	r9, #0
	jgt	inverse	
	mov	a, r8	
	ret		

enterr:	ld	a, #10	;перевод курсора на строку ниже
	wd	#0
	jmp	enterOK

inverse:	subr	r0, r8	
	mov	a, r0
	clear	r0
	ret

Space:	equ	32	
Enter:	equ	13	
Minus:	equ	45	
limit:	equ	214