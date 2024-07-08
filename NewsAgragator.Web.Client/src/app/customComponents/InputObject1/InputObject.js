import React, { useEffect, useState, useRef } from 'react';

import './InputObject.css';

const InputObject = ({ value, options, placeholder, onClick }) => {
    const [show, setShow] = useState(false);
    const [query, setQuery] = useState('');
    const [selectedObject, setSelectedObject] = useState({Id:'',Text:''});
    const textInput = useRef(null);

    const toggle = () => {
        setShow(!show);
        textInput.current.focus();
    };

    const editQuery = (query) => {
        setQuery(query);
    };

    const getText = () => {
        if (!value) {
            return placeholder;
        }
        let val = options && options.find((o) => o.id === value);
        if (!val) {
            return placeholder;
        }
        return val.text;
    };

    const select = (id,text) => {
        console.log("Select");
        console.log(selectedObject.Id, selectedObject.Text);
        console.log(id, text);
        setSelectedObject({ Id: id, Text: text });
        onClick(id, text);
        setShow(false);
    };

    const getOptions = () => {
        return options && options.filter((o) => o.text.toLowerCase().includes(query.toLowerCase())).map((x) => (
            <div key={x.id} onClick={() => select(x.id, x.text)}>{x.text}</div>
        ));
    };

    const getConstantOptions = () => {
        return [{ id: '', text: 'Any' }].map((x) => (
            <div key={x.id} onClick={() => select(x.id)}>{x.text}</div>
        ));
    };

    return (
        <div className="">
            <div className="" onClick={toggle}>
                <span style={{ display: show ? 'none' : 'block' }}>{getText()}</span>
                <input
                    style={{ display: show ? 'block' : 'none' }}
                    onClick={(e) => e.stopPropagation()}
                    ref={textInput}
                    className=""
                    type="text"
                    onChange={(e) => editQuery(e.target.value)}
                />
                <div className="" style={{ display: show ? 'block' : 'none' }}>
                    <div className="">
                        {getConstantOptions()}
                        {getOptions()}
                    </div>
                </div>
            </div>
        </div>
    );
};

export default InputObject;