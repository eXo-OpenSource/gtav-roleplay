import React, { useState } from "react";
import SimpleBar from "simplebar-react";
import { DragDropContext, Droppable, Draggable } from "react-beautiful-dnd"

function Inventory() {

    const init = [...Array(15).keys()];

    const reorder = (list, startIndex, endIndex) => {
        const result = Array.from(list);
        const [removed] = result.splice(startIndex, 1);
        result.splice(endIndex, 0, removed);
      
        return result;
      };

    const [items, setItems] = useState(init)

    function getStuff() {
        return items.map((value, index) => {
            return <Item item={value} index={index} />
        })
    }

    function onDragEnd(result) {
        if (!result.destination) {
          return;
        }
    
        if (result.destination.index === result.source.index) {
          return;
        }
    
        const reordered = reorder(
          items,
          result.source.index,
          result.destination.index
        );
    
        setItems(reordered)
      }
    

    return (
        <div className="container mx-auto max-w-md bg-gray-700 mt-32 py-2" style={{"height": "60vh"}}>
            <DragDropContext onDragEnd={onDragEnd}>
                <SimpleBar style={{"maxHeight": "60vh"}}>
                <Droppable droppableId="inv">
                {(provided, snapshot) => (
                    <div
                    ref={provided.innerRef}
                    className={(snapshot.isDraggingOver ? 'bg-blue-700' : 'bg-gray-700') + " px-2 rounded mx-2" }
                    {...provided.droppableProps}
                    >
                    {getStuff()}
                    {provided.placeholder}
                    </div>
                )}
                </Droppable>
                </SimpleBar>
            </DragDropContext>
        </div>
    );
}

function Item({item, index}) {
    return (
        <Draggable draggableId={item+"s"} key={item} index={index}>
            {(provided, snapshot) => (
              <div
                className="bg-gray-800 rounded mb-2 px-4 py-4 text-white"
                ref={provided.innerRef}
                {...provided.draggableProps}
                {...provided.dragHandleProps}
              >
                Drag me!{item}
              </div>
            )}
          </Draggable>
    )
}

export default Inventory;