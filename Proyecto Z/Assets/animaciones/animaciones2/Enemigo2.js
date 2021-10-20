#pragma strict

var Vida = 100;//vida enemigo

var anim : Animator;

var nav : NavMeshAgent;
var player : Transform;//poner objeto al que sigue el enemigo

var vidaPlayer : PlayerVida;//script vida del player
var ataque : int;//vida que saca el enemigo al player al atacar
var AtacBool : boolean;

var character : CharacterController;

var distancia : float;//distancia que hay entre el player y el enemigo

var muerte : int;

function Start () {
muerte = Random.Range (1, 3);
character.enabled = true;
AtacBool = false;
}

function Update () {

//CALCULAR DISTANCIA
distancia = Vector3.Distance(transform.position, player.position);

//SEGUIR AL PLAYER
if(Vida >= 1){// si la vida del enemigo es mayor de 1, el enemigo seguira al player
nav.destination = player.position;
}

if(distancia > 3){// si distancia es mas grande de 3
nav.speed = 2;     
anim.SetBool("ataque", false);
}

//ATAQUE
if(distancia < nav.stoppingDistance && AtacBool == false){
nav.speed = 0;
anim.SetBool("ataque", true);

Invoke("Ataque", 1.1);//tiempo que tarda en dar el golpe
AtacBool = true;
}

//VIDA
if(Vida <=0){

nav.speed = 0;
Invoke("Muerto", 5);//tiempo que tarda en desaparecer una vez a llegado su vida a 0
character.enabled = false;

 if(muerte == 1){
 anim.SetBool("muerte1", true);
 }

 if(muerte == 2){
 anim.SetBool("muerte2", true);
 }
}
}

function Muerto () {
//Destroy(gameObject);  //Opcion1
gameObject.SetActive(false);  //Opcion2
}

function Ataque () {
Invoke("AF", 1.5);
if(distancia <= 4 && AtacBool == true){
vidaPlayer.vida -= ataque;
}
}

function AF () {
AtacBool = false;
}

function FV (Dano : int) {
Vida -= Dano;
}