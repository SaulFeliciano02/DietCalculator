cargar(A):-exists_file(A),consult(A).

existe(X,[X|_]).
existe(X,[_|Cola]):-existe(X,Cola).

existeEnLista([],_).
existeEnLista([H|T],L) :- existeEnLista(T,L), existe(H,L).

sonParteDeReceta(List1,Resultado) :-
    ingredientes(Resultado,List2),
    forall(member(Element,List1),
           member(Element,List2)).

noExisteEnLista([],_).
noExisteEnLista([H|T],L) :- noExisteEnLista(T,L), not(existe(H,L)).

%%%% P R I M E R   P U N T O
poseeUnIngrediente(Ingrediente,Resultado) 
  :- ingredientes(Resultado,L),
     existe(Ingrediente,L).

%%%% S E G U N D O   P U N T O
poseeTresIngredientes(X,Resultado) 
  :- ingredientes(Resultado,L),
     existeEnLista(L,X).

%%%% T E R C E R  P U N T O
poseeHerramienta(Herramienta,Resultado) 
  :- herramientas(Resultado,L),
     existe(Herramienta,L).

%%%% C U A R T O  P U N T O
noPoseeHerramienta(Herramienta,Resultado) 
  :- herramientas(Resultado,L),
     not(existe(Herramienta,L)).

%%%% Q U I N T O  P U N T O
noPoseeIngrediente(Ingrediente,Resultado) 
  :- ingredientes(Resultado,L),
     not(existe(Ingrediente,L)).

%%%% S E X T O  P U N T O
noPoseeIngredienteHerramienta(Ingrediente, Herramienta,Resultado)
  :- ingredientes(Resultado,L),
     not(existe(Ingrediente,L)),
     herramientas(Resultado,L2),
     not(existe(Herramienta,L2)).

%%%% E X T R A
noPoseeMultiplesIngredientes(X) 
  :- ingredientes(Resultado,L),
     noExisteEnLista(L,X),
     write(Resultado),nl,fail.
