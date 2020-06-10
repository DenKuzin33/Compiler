
grammar Compiler;

@header{
    using System.Collections;
}

@members{
    public static Hashtable memory = new Hashtable();
}

options
{
    language = CSharp;
}

@namespace { Generated }

compiler
        : statement+;

statement
    : WRITE LP ID RP EOS
    | while
    | if
    | break = BREAK EOS
    | cont = CONTINUE EOS
    | ID ASSIGN assignment EOS
    ;

assignment
    : cond = condition
    | exp = expression
    ;

expression 
    : left = expression sign = DIV right = expression
    | left = expression sign = MULT right = expression
    | left = expression sign = ADD right = expression
    | left = expression sign = SUB right = expression
    | LP expression RP
    | number
    | id = ID
    ;

number 
    : INT
    | FLOAT 
    ;

condition
    : left = expression not = NOT? EQUALS right = expression
    | left = expression gt = GT right = expression
    | left = expression lt = LT right = expression
    | leftC = condition AND rightC = condition
    | leftC = condition OR rightC = condition
    | not = NOT? BOOL
    | not = NOT? ID
    ;

if 
    : IF LP cond = condition RP LC state = statement RC (ELSE LC altState = statement RC)? 
    ;

while
    : WHILE LP cond = condition RP LC state = statement+ RC
    ;

OR: '||';
AND: '&&';
CONTINUE: 'continue';
BREAK: 'break';
WHILE: 'while';
WRITE: 'Write';
IF: 'if'; 
ELSE: 'else';
BOOL: 'false' | 'true';
ID: [a-zA-Z]+;
INT: [0-9]+;
FLOAT: INT*','INT+;
EOS: ';';
EQUALS: '==';
GT: '>';
LT: '<';
NOT: '!';
ADD: '+';
SUB: '-';
DIV: '/';
MULT: '*';
LP: '(';
RP: ')';
LC: '{';
RC: '}';
ASSIGN: '=';
SPACE:          [ \t\r\n]+    -> channel(HIDDEN);
COMMENT_INPUT:  '/*' .*? '*/' -> channel(HIDDEN);
LINE_COMMENT:   ('-- ' | '#')
                ~[\r\n]*
                (NEWLINE | EOF)
                              -> channel(HIDDEN);
NEWLINE : '\r'? '\n';