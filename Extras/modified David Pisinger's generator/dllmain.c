#include "dll.h"
//#include <windows.h>

#include <stdlib.h>
#include <stdio.h>
#include <stdarg.h>
// #include <values.h>
#include <string.h>
// #include <malloc.h>



/* ======================================================================
				     macros
   ====================================================================== */

#define srand(x)     srand48x(x)
#define randm(x)     (lrand48x() % (x))
#define NO(f,i)      ((int) ((i+1)-f))
#define TRUE  1
#define FALSE 0


/* ======================================================================
				 type declarations
   ====================================================================== */

typedef int   boolean; /* boolean variables */
typedef short itype;   /* item profits and weights */
typedef long  stype;   /* sum of pofit or weight */

typedef struct {
  itype   p;     /* profit */
  itype   w;     /* weight */
  boolean x;     /* solution variable */
} item;

unsigned long _h48, _l48;

void srand48x(long s)
{
  _h48 = s;
  _l48 = 0x330E;
}

long lrand48x(void)
{
  _h48 = (_h48 * 0xDEECE66D) + (_l48 * 0x5DEEC);
  _l48 = _l48 * 0xE66D + 0xB;
  _h48 = _h48 + (_l48 >> 16);
  _l48 = _l48 & 0xFFFF;
  return (_h48 >> 1);
}


void error(char *str, ...)
{
  va_list args;

  va_start(args, str);
  vprintf(str, args); printf("\n");
  va_end(args);
  printf("STOP !!!\n\n"); 
  exit(-1);
}


void pfree(void *p)
{
  if (p == NULL) error("freeing null");
  free(p);
}


void * palloc(size_t no, size_t each)
{
  long size;
  long *p;

  size = no * (long) each;
  if (size == 0) size = 1;
  if (size != (size_t) size) error("alloc too big %ld", size);
  p = malloc(size);
  if (p == NULL) error("no memory size %ld", size);
  return p;
}


void showitems(item *f, item *l, stype c ,  char str[])
{
  item *i;
  stype ps, ws;
  FILE *out;
  
  //printf("%s",path);
  //char path2[] = {"_generatorFiles/"}; // HARDCODED DUE TO CHAR PARAMETER ERROR
  //char fullpath[] = "";
  //strncat(fullpath, path , 150);
  //strncat(fullpath, str , 30);
  strncat(str, ".txt", 4);
  out = fopen(str, "w");
  
  if (out == NULL) error("no file");
  fprintf(out,"%d\n", NO(f,l));
  for (i = f; i <= l; i++) 
  {
    fprintf(out, "%5d %5d %5d\n", NO(f,i), i->p, i->w);
  }
  fprintf(out,"%d\n", c);
  fclose(out);
}


stype maketest(item *f, item *l, int r, int type, int v, int S , int seedOffSet)
{
  register item *i;
  register stype sum;
  stype c;
  itype r1;
	
  srand(v);
  sum = 0; r1 = r/10;

  for (i = f; i <= l; i++) {

    i->w = randm(r) + 1;
    switch (type) {
      case 1: i->p = randm(r) + 1;
	      break;
      case 2: i->p = randm(2*r1+1) + i->w - r1;
	      if (i->p <= 0) i->p = 1;
	      break;
      case 3: i->p = i->w + 10;
	      break;
      case 4: i->p = i->w;
	      break;
    }
    sum += i->w;
  }
  c = (v * (double) sum) / (S + 1);
  if (c <= r) c = r+1;
  return c;
}


int main(int argc, char *argv[])
{

}


DLLIMPORT int genKSFiles(int seedOffSet , char path[])
{

  item *f, *l;
  int n, r, type, i, S;
  stype c;
  S = 5;

  int iter = 1;
  int iPhase = 1;
  int tPhase = 1;
  int rPhase = 50;
  
  printf("%s\n",path);
  
  while(iter <= 320)
  {
  	 if (iter < 80)
  	 {
  	 	n = 10;
	 }
	 else if (iter < 160)
	 {
	 	n = 50;
	 }
	 else if (iter < 240)
	 {
	 	n = 100;
	 }
	 else
	 {
	 	n = 500;
	 }
	 
	 i = iPhase;
	 type = tPhase;
	 r = rPhase;
	 
	 char str[10000] = "";
	 strncat(str, path, 10000);
	 
	 strncat(str, "test_No", 7);
	 //char str[30] = "test_No";
	 
	 char num[3];
	 sprintf(num, "%d", iter);
	 strncat(str, num, 3);
	 strncat(str, "_n", 2);
	 
	 char Nsize[3];
	 sprintf(Nsize, "%d", n);
	 strncat(str, Nsize, 3);
	 strncat(str, "_r", 2 );
	 
	 char Rsize[4];
	 sprintf(Rsize, "%d", r);
	 strncat(str, Rsize, 4);
	 strncat(str, "_t", 2);
	 
	 char Tname[1];
	 sprintf(Tname, "%d", type);
	 strncat(str, Tname, 1);
	 strncat(str, "_i", 2);
	 
	 char Ic[1];
	 sprintf(Ic, "%d", i);
	 strncat(str, Ic, 1);
	 //strncat(str, "", 2);
	 
	 iPhase++;
	 
	 if (iPhase > 5)
	 {
	 	iPhase = 1;
	 	tPhase++;
	 	if (tPhase > 4)
	 	{
	 		tPhase = 1;
	 		if (rPhase == 50)
				rPhase = 100;
			else if (rPhase == 100)
				rPhase = 500;
			else if (rPhase == 500)
				rPhase = 1000;
			else
				rPhase = 50;
		}
	 }
	 
	  f = palloc(n, sizeof(item));
	  l = f + n-1;
	  c = maketest(f, l, r, type, i, S , seedOffSet);
	  showitems(f, l, c,str);
	  pfree(f);
	  iter++;
  }
  
  

  return 0;
}


