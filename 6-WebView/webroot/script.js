
const dr = document.getElementById("Drag")

const onDragStart = evt => { 
    alert("dragStart")
    evt.preventDefault()
}

dr.onmousedown = onDragStart


