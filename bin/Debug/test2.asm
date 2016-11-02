<?xml version="1.0" encoding="utf-8"?>
<project>
  <code>ld c, text
ldb d, textcLen

print:
ld a, c
wd #0
incr c, d
jпе print

text: db 'hello world', '13' 
textcLen: db 12

</code>
</project>