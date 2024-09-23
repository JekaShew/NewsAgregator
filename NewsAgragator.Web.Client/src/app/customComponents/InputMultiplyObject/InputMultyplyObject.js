import React, { useState, useRef } from 'react';

const InputMultyplyObject = ({ data, addValue, removeValue, placeholder }) => {
  const [show, setShow] = useState(false);
  const [query, setQuery] = useState('');
  const textInputRef = useRef(null);

  const toggle = () => {
    setShow(!show);
    textInputRef.current.focus();
  };

  const editQuery = (query) => {
    setQuery(query);
  };

  const toggleValue = (id,text) => {
    if (data.value.includes(id)) {
      removeValue(id,text);
    } else {
      addValue(id,text);
    }
  };

  const setColor = () => {
    console.log(data);
    const val = data.value
      .map((x) => data.options.find((o) => o.id === x)?.text)
      .join(', ');

    return val ? { color: 'black' } : { color: 'grey' };
  };

  const getText = () => {
    const val = data.value
      .map((x) => data.options.find((o) => o.id === x)?.text)
      .join(', ');
    return val || placeholder;
  };

  const getOptions = () => {
    return data.options
      .filter((o) => o.text.toLowerCase().includes(query.toLowerCase()))
      .map((x) => (
        <div key={x.id} onClick={() => toggleValue(x.id,x.text)}>
          {x.text}
        </div>
      ));
  };

  const getConstantOptions = () => {
    return [{ id: '', text: 'Любой' }].map((x) => (
      <div key={x.id} onClick={() => toggleValue(x.id,x.text)}>
        {x.text}
      </div>
    ));
  };

  return (
    <div className="userInputBox">
      <div
        className="userInputDiv"
        onClick={toggle}
        style={setColor()}
      >
        {getText()}
        <div className="inputItems" style={{ display: show ? 'block' : 'none' }}>
          <input
            className="userInput"
            onClick={(e) => e.stopPropagation()}
            ref={textInputRef}
            type="text"
            onChange={(e) => editQuery(e.target.value)}
          />
          <div>
            {getConstantOptions()}
            {getOptions()}
          </div>
        </div>
      </div>
    </div>
  );
};

export default InputMultyplyObject;


// import React, { useState, useRef } from 'react';

// const InputMultyplyObject = ({ data, addValue, removeValue, placeholder }) => {
//   const [show, setShow] = useState(false);
//   const [query, setQuery] = useState('');
//   const textInputRef = useRef(null);

//   const toggle = () => {
//     setShow(!show);
//     textInputRef.current.focus();
//   };

//   const editQuery = (query) => {
//     setQuery(query);
//   };

//   const toggleValue = (value) => {
//     if (data.value.includes(value)) {
//       removeValue(value);
//     } else {
//       addValue(value);
//     }
//   };

//   const setColor = () => {
//     const val = data.value
//       .map((x) => data.options.find((o) => o.value === x)?.text)
//       .join(', ');

//     return val ? { color: 'black' } : { color: 'grey' };
//   };

//   const getText = () => {
//     const val = data.value
//       .map((x) => data.options.find((o) => o.value === x)?.text)
//       .join(', ');
//     return val || placeholder;
//   };

//   const getOptions = () => {
//     return data.options
//       .filter((o) => o.text.toLowerCase().includes(query.toLowerCase()))
//       .map((x) => (
//         <div key={x.value} onClick={() => toggleValue(x.value)}>
//           {x.text}
//         </div>
//       ));
//   };

//   const getConstantOptions = () => {
//     return [{ value: '', text: 'Любой' }].map((x) => (
//       <div key={x.value} onClick={() => toggleValue(x.value)}>
//         {x.text}
//       </div>
//     ));
//   };

//   return (
//     <div className="userInputBox">
//       <div
//         className="userInputDiv"
//         onClick={toggle}
//         style={setColor()}
//       >
//         {getText()}
//         <div className="inputItems" style={{ display: show ? 'block' : 'none' }}>
//           <input
//             className="userInput"
//             onClick={(e) => e.stopPropagation()}
//             ref={textInputRef}
//             type="text"
//             onChange={(e) => editQuery(e.target.value)}
//           />
//           <div>
//             {getConstantOptions()}
//             {getOptions()}
//           </div>
//         </div>
//       </div>
//     </div>
//   );
// };

// export default InputMultyplyObject;




// import React, { useEffect, useState, useRef } from 'react';

// import './InputObject.css';

// const InputObject = ({ value, options, placeholder, onClick }) => {
//     const [show, setShow] = useState(false);
//     const [query, setQuery] = useState('');
//     const [selectedObject, setSelectedObject] = useState({ Id: '', Text: '' });
//     const textInput = useRef(null);

//     const toggle = () => {
//         setShow(!show);
//         textInput.current.focus();
//     };

//     const editQuery = (query) => {
//         setQuery(query);
//     };

//     const getText = () => {
//         if (!value) {
//             return placeholder;
//         }
//         let val = options && options.find((o) => o.id === value);
//         if (!val) {
//             return placeholder;
//         }
//         return val.text;
//     };

//     const select = (id, text) => {
//         console.log("Select");
//         setSelectedObject({ Id: id, Text: text });
//         console.log(selectedObject.Id, selectedObject.Text);
//         console.log(id, text);

//         onClick(id, text);
//         setShow(false);
//     };

//     const getOptions = () => {
//         return options && options.filter((o) => o.text != null ? o.text.toLowerCase().includes(query.toLowerCase()) : o.text = "No data").map((x) => (
//             <div key={x.id} onClick={() => select(x.id, x.text)}>{x.text}</div>
//         ));
//     };

//     const getConstantOptions = () => {
//         return [{ id: '', text: 'Any' }].map((x) => (
//             <div key={x.id} onClick={() => select(x.id, x.text)}>{x.text}</div>
//         ));
//     };

//     return (
//         <div className="">
//             <div className="" onClick={toggle}>
//                 <span style={{ display: show ? 'none' : 'block' }}>{selectedObject.Text == '' ? getText() : selectedObject.Text}</span>
//                 <input
//                     style={{ display: show ? 'block' : 'none' }}
//                     onClick={(e) => e.stopPropagation()}
//                     ref={textInput}
//                     className=""
//                     type="text"
//                     onChange={(e) => editQuery(e.target.value)}
//                 />
//                 <div className="" style={{ display: show ? 'block' : 'none' }}>
//                     <div className="">
//                         {getConstantOptions()}
//                         {getOptions()}
//                     </div>
//                 </div>
//             </div>
//         </div>
//     );
// };

// export default InputObject;