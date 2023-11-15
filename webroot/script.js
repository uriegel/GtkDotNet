// const button = document.getElementById("button")
// const devTools = document.getElementById("devTools")
//button.onclick = () => webWindowNetCore.postMessage("Guten Abend!👌👌👌😜") 


//webWindowNetCore.setCallback(text => alert(text))

// button.onclick = () => alert(`Das is es: ${affe}`)
// devTools.onclick = () => alert("devTools")

//alert("anfang")

//console.log("Affe1")

const dr = document.getElementById("Drag")
//drag.onmousedown = () => alert("dragStart")

const onDragStart = evt => { 
//    evt.dataTransfer.setData("internalCopy", "true")
    alert("dragStart")
    evt.preventDefault()
}


dr.ondragstart = onDragStart
// dr.ondragend = () => alert("dragEnd")


