import React from 'react';

const ModalWrapper = ({ isOpen, onClose, children }) => {
  if (!isOpen) return null;

  return (
    <div className="modal-overlay">
      <div className="modal-content">
        <button className="modal-close-button" onClick={onClose}>
         Close
        </button>
        {children}
      </div>
    </div>
  );
};

export default ModalWrapper;