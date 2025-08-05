<!DOCTYPE html>
<html>

<head>

</head>

# Crossy_Road_Mobile
Game criando nas aulas do curso Crossy Road 3D versão mobile no site: https://aprendaunity.com.br, nesse curso eu aprendi a usar técnica geração procedural na fase no game.

<h2>Sumário</h2>
    <ol>
        <li><h4><a href="#C1">Menu Game</a></h4></li>
        <li><h4><a href="#C2">Player</a></h4></li>
        <li><h4><a href="#C3">GameController</a></h4></li>
        <li><h4><a href="#C4">HUD GamePlay</a></h4></li>
        <li><h4><a href="#C5">Animation Player</a></h4></li>
        <li><h4><a href="#C6">Itens and Collectibles</a></h4></li>
        <li><h4><a href="#C7">Cars and Truck, Train</a></h4></li>
        <li><h4><a href="#C8">Fx and Music</h4></li>
    </ol>

<h1 id="C1">Menu</h1>

<p>O menu do jogo vai ter um setas apontada para direita e outra para esquerda e no meu da tela vai mostrar 3 personagens e quando o jogador click vai mudar de personagem para o lado que estiver o personagem, os personagens vão estar                    trancados para liberar ele com moeda do jogo vai aparecer um cadeado no meu do personagem e embaixo o preço do personagem. Vai ter um botão jogar se jogador click entra no jogo e quando o, jogador escolher um personagem                               cadeado o botão jogador mudar para liberar e o jogador tiver o dinheiro para liberar o personagem ele poderá jogar com ele.</p>

<img src="https://github.com/guiardev/Crossy_Road_Mobile/blob/main/Assets/Recordings/Menu.gif" width="341" height="606"/>
  
<h3>Script Menu</h3>

<p>O script que vai ser responsável por fazer o menu funcional</p>
  
<img src="https://github.com/guiardev/Crossy_Road_Mobile/blob/main/imgs/img_menuController.png" width="510" height="766"/>

<h3>Botões do Menu</h3>

<p>Aqui estão as configurações dos botões que o jogador vai interagir.</p>

<table border="0">
    <tr>
      <td><img src="https://github.com/guiardev/Crossy_Road_Mobile/blob/main/imgs/img_btnAction.png" width="510" height="286"/></td>
      <td><img src="https://github.com/guiardev/Crossy_Road_Mobile/blob/main/imgs/img_btn_left.png" width="510" height="396"/></td>
      <td><img src="https://github.com/guiardev/Crossy_Road_Mobile/blob/main/imgs/img_btn_right.png" width="510" height="396"/></td>
    </tr>
</table>

<h1 id="C2">Player</h1>

<p>As configurações do personagem</p>

<h3>Script player e meshPlayer</h3>

<table border="0">
    <tr>
      <td><img src="https://github.com/guiardev/Crossy_Road_Mobile/blob/main/imgs/img_player.png" width="512" height="400"/></td>
      <td><img src="https://github.com/guiardev/Crossy_Road_Mobile/blob/main/imgs/img_meshPlayer.png" width="511" height="446"/></td>
    </tr>
</table>

<h3>O player vai ser controlado pela hud que vai ter setas que o jogador vai clicar e o player vai mover para o lado da seta</h3>

<p>As configurações como o jogador vai controlar o personagem</p>

<table border="0">
    <tr>
      <td><img src="https://github.com/guiardev/Crossy_Road_Mobile/blob/main/imgs/img_Up.png" width="511" height="289"/></td>
      <td><img src="https://github.com/guiardev/Crossy_Road_Mobile/blob/main/imgs/img_Down.png" width="511" height="289"/></td>
    </tr>
    <tr>
      <td><img src="https://github.com/guiardev/Crossy_Road_Mobile/blob/main/imgs/img_Right.png" width="511" height="289"/></td>
      <td><img src="https://github.com/guiardev/Crossy_Road_Mobile/blob/main/imgs/img_Left.png" width="511" height="289"/></td>
    </tr>
</table>
     
</html>
