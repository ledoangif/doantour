<template>
    <CVModal id_model="create-update-Country-modal">
        <template #icon>
            <slot name="icon"></slot>
        </template>
        <template #title>
            <span v-if="!isEditMode">
                <slot name="header">Thêm quốc gia</slot>
            </span>
            <span v-else>
                <slot name="header">Cập nhật quốc gia</slot>
            </span>
        </template>
        <template #body>
            <Form @submit="handleSubmit">
                <div class="row">
                    <div class="row col-lg-4 mb-3 form-group required">
                        <div class="row mb-3 form-group required">
                            <label
                                for="source-name"
                                class="col-sm-5 col-form-label control-label text-end"
                            >
                                Tên quốc gia
                            </label>
                            <div class="col-sm-7">
                                <Field
                                    name="countryName"
                                    :rules="{ required: true}"
                                    v-model="Country.countryName"
                                    type="text"
                                    class="form-control"
                                    
                                />
                                <ErrorMessage name="countryName" class="text-danger" />
                            </div>
                        </div>
                    </div>
                    <div class="row col-lg-4 mb-3 form-group required">
                        <div class="row mb-3 form-group required">
                            <label
                                for="source-name"
                                class="col-sm-5 col-form-label control-label text-end"
                            >
                                Ảnh quốc gia
                            </label>
                            <div class="col-sm-7">
                                <Field
                                    name="countryImage"
                                    type="file"
                                    :rules="{ required: true}"
                                    class="form-control"
                                    @change="handlefiles"
                                    accept="image/jpeg, image/png, image/jpg"
                                    
                                />
                                <ErrorMessage name="countryImage" class="text-danger" />
                            </div>
                        </div>
                    </div>
                    <div class="row col-lg-4 mb-3 form-group required">
                        <div class="row mb-3 form-group required">
                            <label
                                for="source-name"
                                class="col-sm-5 col-form-label control-label text-end"
                            >
                                Tên châu lục
                            </label>
                            <div class="col-sm-7">
                                <Field
                                        name="continentName"
                                        :rules="{ required: true}"
                                        v-model="Country.continentName"
                                        as="select"
                                        class="form-control"

                                    >
                                        <option
                                            v-for="item in Continent"
                                            class="text-dark"
                                            :value="item.value"
                                        >
                                            {{ item.value }}
                                        </option>
                                    </Field>
                                <ErrorMessage name="continentName" class="text-danger" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer align-content-center justify-content-center">
                    <button
                        v-if="isEditMode"
                        type="submit"
                        class="btn btn-sm btn-primary d-flex align-items-center"
                        data-bs-dismiss="modal"
                    >
                        Lưu
                    </button>
                    <button
                        v-else
                        type="submit"
                        class="btn btn-sm btn-primary d-flex align-items-center"
                        data-bs-dismiss="modal"
                    >
                        Thêm
                    </button>
                    <button
                        type="reset"
                        class="btn btn-sm btn-outline-primary d-flex align-items-center"
                        data-bs-dismiss="modal"
                    >
                        Huỷ bỏ
                    </button>
                </div>
            </Form>
        </template>
    </CVModal>
</template>

<script setup>
import { ref, watch } from 'vue';
import Api from '~/service/Base/api.ts';
import { Form, Field, ErrorMessage } from 'vee-validate';
const api = new Api();
const emits = defineEmits(['Country-saved']);

const props = defineProps({
    editCountry: {
        type: Object,
        default: null,
        required: false,
    },
    isEditMode: {
        type: Boolean,
        default: false,
        required: true,
    },
});

const resetForm = () => {
    Country.value.countryName = '';
    Country.value.countryImage = '';
    Country.value.continentName = '';
    errorMessage.value = '';
};
const formRef = ref(null);


const errorMessage = ref('');
const Country = ref({
    countryName: '',
    countryImage: '',
    continentName: '',
});

const Continent = [
    { id: 1, value: 'Châu Á' },
    { id: 2, value: 'Châu Âu' },
    { id: 3, value: 'Châu Mỹ' },
    { id: 4, value: 'Châu Phi' },
];

const handlefiles = async (event) => {
    const input = event.target;
    const file = input.files?.[0];

    // Kiểm tra định dạng ảnh
    const allowedFormats = ['jpg', 'jpeg', 'png'];
    const fileExtension = file?.name.split('.').pop()?.toLowerCase();

    if (!fileExtension || !allowedFormats.includes(fileExtension)) {
        alert('Chỉ chấp nhận các định dạng ảnh JPG, PNG');
        return;
    }

    // Thực hiện upload ảnh
    const formData = new FormData();
    formData.append('ImageFile', file);

    try {
        const response = await api.postAPI('/Country/UploadFile', formData);
        Country.value.countryImage = response.data; // Lưu đường dẫn ảnh trả về vào Country
        errorMessage.value = ''; // Reset thông báo lỗi
    } catch (error) {
        console.error('Upload image failed:', error);
        errorMessage.value = 'Lỗi khi tải lên hình ảnh. Vui lòng thử lại.';
        //alert('Lỗi khi tải lên hình ảnh. Vui lòng thử lại.');
    }
};
// const resetBodyOverflow = () => {
//     document.body.style.overflow = 'auto';  // Đảm bảo body có thể cuộn
// };

const createCountry = async () => {
   // const startTime = performance.now(); // Lưu thời gian bắt đầu
    try {
        const formData = new FormData();
        formData.append('countryName', Country.value.countryName);
        formData.append('countryImage', Country.value.countryImage);
        formData.append('continentName', Country.value.continentName);

        // Gửi yêu cầu tạo quốc gia
        await api.postAPI('/Country/InsertCountry', formData);

        // Thông báo đã lưu thành công, đóng modal và reset form
        emits('Country-saved');
        $('#create-update-Country-modal').modal('hide');
            // setTimeout(() => {
            //     const modalBackdrop = document.querySelector('.modal-backdrop');
            //     if (modalBackdrop) {
            //         modalBackdrop.remove(); // Loại bỏ backdrop sau khi modal đóng
            //     }
            //     resetBodyOverflow(); // Đảm bảo body có thể cuộn lại
            //     resetForm();
            //     // Tạo lại backdrop và mở modal
            //     const newBackdrop = document.createElement('div');
            //     newBackdrop.classList.add('modal-backdrop', 'fade', 'show');
            //     document.body.appendChild(newBackdrop); // Thêm lại backdrop thủ công
            //     $('#create-update-Country-modal').modal('show'); // Mở modal
            //     const endTime = performance.now(); // Lưu thời gian kết thúc bên trong setTimeout
            //     console.log(`Thời gian chạy: ${(endTime - startTime).toFixed(2)}ms`); // Log thời gian chạy
            // }, 600); // Đảm bảo backdrop bị xóa sau một khoảng thời gian ngắn
    } catch (error) {
        console.error('Error creating Country:', error);
    }
    resetForm();
};

const updateCountry = async () => {
    const data = {
        countryName: Country.value.countryName,
        countryImage: Country.value.countryImage,
        continentName: Country.value.continentName,
        id: props.editCountry.id,
    };
    try {
        await api.putAPI(`/Country/${props.editCountry.id}`, data);
        emits('Country-saved');
        $('#create-update-Country-modal').modal('hide');
    } catch (error) {
        console.error('Error updating Country:', error);
        errorMessage.value = "Đã xảy ra lỗi khi cập nhật quốc gia. Vui lòng thử lại.";
    }
    resetForm();
};
const handleSubmit = async () => {
    if (props.isEditMode) {
        // Chế độ cập nhật
        await updateCountry();
    } else {
        // Chế độ tạo mới
        await createCountry();
    }
};
watch(
    () => props.editCountry,
    (newVal) => {
        if (newVal) {
            Country.value = { ...newVal };
        }
    },
    { immediate: true },
);
</script>
