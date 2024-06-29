import React, { useState } from 'react';
import api from '../../../api';

const CreateImageProduct = ({productId, fetchProducts}) => {
  const [selectedFile, setSelectedFile] = useState(null);
  const [nameImage, setNameImage] = useState('');
  const [Expanded , SetExpanded] = useState(false)

  const handleUpload = async () => {
    if (!selectedFile) {


      return;
    }
      
    const formData = new FormData();
    formData.append('productd', productId);
    formData.append('ImageFile', selectedFile);
    formData.append('ImageName', nameImage);

    try {
      const config = {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      };
      await api.post(`/api/products/${productId}/images`, formData, config);
      fetchProducts();
    } catch (error) {
      console.error('Error add image', error);
    }
  };

  return (
    <div>
      <button onClick={()=>{SetExpanded(!Expanded)}} className='Add-Image'>Add Image</button>
      {Expanded && <>
      <input
        type='text'
        placeholder='Name Image'
        className='ImageName-Input'
        value={nameImage}
        onChange={(e) => setNameImage(e.target.value)}
      />
      <input type="file" className='Image-Input' onChange={(e) => setSelectedFile(e.target.files[0])}/>
      <button onClick={handleUpload} className="Upload-Image" >Upload Image</button>
      </>}
    </div>
  );
};

export default CreateImageProduct;
