import React, { useState } from "react";
import SimpleBar from "simplebar-react";
import { DragDropContext, Droppable, Draggable } from "react-beautiful-dnd"

function Inventory() {

    const init = [
      {
        id: 1,
        item: {
          name: "kekse"
        },
      },
      {
        id: 5,
        item: {
          name: "kekse"
        },
      },
    ];

    const reorder = (list, startIndex, endIndex) => {
        const result = Array.from(list);
        const [removed] = result.splice(startIndex, 1);
        result.splice(endIndex, 0, removed);
      
        return result;
      };

    const [items, setItems] = useState(init)

    function getStuff() {
        return [...Array(15).keys()].map((value, index) => {
            const item = items.find((item) => item.id == index)?.item;
            return <Droppable droppableId={index + ""} isDropDisabled={item != undefined} key={index}>
            {(provided, snapshot) => (
                <div
                ref={provided.innerRef}
                className={(snapshot.isDraggingOver ? 'bg-blue-700' : 'bg-gray-800') + " px-2 mx-2 w-24 h-24 mb-2 " + (Boolean(snapshot.draggingFromThisWith) ? 'bg-green-700' : '')}
                {...provided.droppableProps}
                >
                {item != undefined ? <Item item={item} index={index} /> : null}
                <span style={{ display: 'none' }}>{provided.placeholder}</span>
                </div>
            )}
            </Droppable>
        })
    }

    function onDragEnd(result) {
        if (!result.destination) {
          return;
        }
    
        if (result.destination.droppableId === result.source.droppableId) {
          return;
        }

        const oldItem = items.find((item) => item.id == result.source.droppableId);
        const newItems = items.filter((item) => item.id != result.source.droppableId);
        
        newItems.push({
          id: result.destination.droppableId,
          item: oldItem.item
        })

        setItems(newItems)
    
        //setItems(reordered)
      }
    

    return (
        <div className="container mx-auto bg-gray-700 mt-64 py-2" style={{"height": "50vh", "width": "28vw"}}>
            <DragDropContext onDragEnd={onDragEnd}>
                <SimpleBar style={{"maxHeight": "49vh"}}>
                    <div className="flex flex-wrap px-4">
                      {getStuff()}
                    </div>
                </SimpleBar>
            </DragDropContext>
        </div>
    );
}

function Item({item, index}) {
    return (
        <Draggable draggableId={index+item.name+"s"} key={item} index={index}>
            {(provided, snapshot) => (
              /*<div
                className="bg-gray-800 rounded mb-2 px-4 py-4 text-white"
                ref={provided.innerRef}
                {...provided.draggableProps}
                {...provided.dragHandleProps}
              >
                Drag me!{item}
              </div>*/
              <div className="relative text-white" ref={provided.innerRef}
              {...provided.draggableProps}
              {...provided.dragHandleProps}>
                <img className="w-24 h-24 px-2 py-2 shadow-img" src={"https://static.exo.cool/exov-static/images/vehicles/interaction/trunk.png"} />
                <span className="font-bold absolute right-0 bottom-0">22</span>
              </div>
            )}
          </Draggable>
    )
}

export default Inventory;