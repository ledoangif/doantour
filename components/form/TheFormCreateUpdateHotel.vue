<template>
    <Form @submit="handleSubmit">
        <CVModal id_model="create-update-Hotel-modal">
            <template #icon>
                <slot name="icon"></slot>
            </template>
            <template #title>
                <span v-if="!isEditMode">
                    <slot name="header">Thêm khách sạn</slot>
                </span>
                <span v-else>
                    <slot name="header">Cập nhật khách sạn</slot>
                </span>
            </template>
            <template #body>
                <div class="row">
                    <div class="row col-lg-8 mb-3 form-group required">
                        <div class="row mb-3 form-group required">
                            <label
                                for="source-name"
                                class="col-sm-4 col-form-label control-label text-end"
                            >
                                Tên khách sạn
                            </label>
                            <div class="col-sm-8">
                                <Field
                                    name="hotelName"
                                    v-model="Hotel.hotelName"
                                    type="text"
                                    class="form-control"
                                    :rules="{required : true,onlyCharacters:true}"
                                />
                                <ErrorMessage name="hotelName" class="text-danger" />
                            </div>
                        </div>
                    </div>
                    <div class="row col-lg-4 mb-3 form-group required">
                        <div class="row mb-3 form-group required">
                            <label
                                for="source-name"
                                class="col-sm-4 col-form-label control-label text-end"
                            >
                                Số sao
                            </label>
                            <div class="col-sm-8">
                                <Field
                                    name="rate"
                                    v-model="Hotel.rate"
                                    class="form-control"
                                    type="number"
                                :rules="{required : true, point : true}"
                                />
                                <ErrorMessage name="rate" class="text-danger" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="row col-lg-8 mb-3 form-group required">
                        <div class="row mb-3 form-group required">
                            <label
                                for="source-name"
                                class="col-sm-4 col-form-label control-label text-end"
                            >
                                Link Page
                            </label>
                            <div class="col-sm-8">
                                <Field
                                    name="linkHotel"
                                    v-model="Hotel.linkHotel"
                                    type="text"
                                    class="form-control"
                                    :rules="{required : true}"
                                />
                                <ErrorMessage name="linkHotel" class="text-danger" />
                            </div>
                        </div>
                    </div>
                    <div class="row col-lg-4 mb-3 form-group required">
                        <div class="row mb-3 form-group required">
                            <label
                                for="source-name"
                                class="col-sm-4 col-form-label control-label text-end"
                            >
                                Chủ sở hữu
                            </label>
                            <div class="col-sm-8">
                                <Field
                                    name="ceo"
                                    v-model="Hotel.ceo"
                                    type="text"
                                    class="form-control"
                                    :rules="{required : true}"
                                />
                                <ErrorMessage name="ceo" class="text-danger" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="row col-lg-8 mb-3 form-group required">
                        <div class="row mb-3 form-group required">
                            <label
                                for="source-name"
                                class="col-sm-4 col-form-label control-label text-end"
                            >
                                Địa chỉ
                            </label>
                            <div class="col-sm-8">
                                <Field
                                    name="address"
                                    v-model="Hotel.address"
                                    type="text"
                                    class="form-control"
                                    :rules="{required : true}"
                                />
                                <ErrorMessage name="address" class="text-danger" />
                            </div>
                        </div>
                    </div>
                    <div class="row col-lg-4 mb-3 form-group required">
                        <div class="row mb-3 form-group required">
                            <label
                                for="source-name"
                                class="col-sm-4 col-form-label control-label text-end"
                            >
                                Hotline
                            </label>
                            <div class="col-sm-8">
                                <Field
                                    name="hotline"
                                    v-model="Hotel.hotline"
                                    type="text"
                                    class="form-control"
                                    :rules="{required : true}"
                                />
                                <ErrorMessage name="hotline" class="text-danger" />
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
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            width="0.88em"
                            height="1em"
                            viewBox="0 -70 700 700"
                        >
                            <path
                                fill="#f7f7f7"
                                d="m433.941 129.941l-83.882-83.882A48 48 0 0 0 316.118 32H48C21.49 32 0 53.49 0 80v352c0 26.51 21.49 48 48 48h352c26.51 0 48-21.49 48-48V163.882a48 48 0 0 0-14.059-33.941M224 416c-35.346 0-64-28.654-64-64c0-35.346 28.654-64 64-64s64 28.654 64 64c0 35.346-28.654 64-64 64m96-304.52V212c0 6.627-5.373 12-12 12H76c-6.627 0-12-5.373-12-12V108c0-6.627 5.373-12 12-12h228.52c3.183 0 6.235 1.264 8.485 3.515l3.48 3.48A11.996 11.996 0 0 1 320 111.48"
                            />
                        </svg>
                        Lưu
                    </button>
                    <button
                        v-else
                        type="submit"
                        class="btn btn-sm btn-primary d-flex align-items-center"
                        data-bs-dismiss="modal"
                    >
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            width="10px"
                            height="10px"
                            viewBox="0 -70 700 700"
                        >
                            <path
                                fill="#f7f7f7"
                                d="m433.941 129.941l-83.882-83.882A48 48 0 0 0 316.118 32H48C21.49 32 0 53.49 0 80v352c0 26.51 21.49 48 48 48h352c26.51 0 48-21.49 48-48V163.882a48 48 0 0 0-14.059-33.941M224 416c-35.346 0-64-28.654-64-64c0-35.346 28.654-64 64-64s64 28.654 64 64c0 35.346-28.654 64-64 64m96-304.52V212c0 6.627-5.373 12-12 12H76c-6.627 0-12-5.373-12-12V108c0-6.627 5.373-12 12-12h228.52c3.183 0 6.235 1.264 8.485 3.515l3.48 3.48A11.996 11.996 0 0 1 320 111.48"
                            />
                        </svg>
                        Thêm
                    </button>
                    <button
                        type="reset"
                        class="btn btn-sm btn-outline-primary d-flex align-items-center"
                        data-bs-dismiss="modal"
                    >
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            viewBox="0 -70 700 700"
                            class="icon"
                            width="10px"
                            height="10px"
                        >
                            <path
                                fill="#006eff"
                                d="m257.5 445.1l-22.2 22.2c-9.4 9.4-24.6 9.4-33.9 0L7 273c-9.4-9.4-9.4-24.6 0-33.9L201.4 44.7c9.4-9.4 24.6-9.4 33.9 0l22.2 22.2c9.5 9.5 9.3 25-.4 34.3L136.6 216H424c13.3 0 24 10.7 24 24v32c0 13.3-10.7 24-24 24H136.6l120.5 114.8c9.8 9.3 10 24.8.4 34.3"
                            />
                        </svg>
                        Huỷ bỏ
                    </button>
                </div>
            </template>
        </CVModal>
    </Form>
</template>

<script setup>
import { ref, watch } from 'vue';
import Api from '~/service/Base/api.ts';
import { Form, Field,ErrorMessage } from 'vee-validate';
const api = new Api();
const emits = defineEmits(['Hotel-saved']);

/** received data */
const props = defineProps({
    editHotel: {
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

const formRef = ref(null);
const selectedfile = ref(null);
/** reset form */
const resetForm = () => {
    if (formRef.value) {
        formRef.value.resetForm();
    }
};

/**
 * define Hotel
 */
const Hotel = ref({
    hotelName: '',
    linkHotel: '',
    rate: '',
    address: '',
    ceo: '',
    hotline: '',
});

/** validate */

/**
 * insert Hotel
 */
const createHotel = () => {
 

    const formData = new FormData();
    formData.append('hotelName', Hotel.value.hotelName);
    formData.append('linkHotel', Hotel.value.linkHotel);
    formData.append('address', Hotel.value.address);
    formData.append('rate', Hotel.value.rate);
    formData.append('ceo', Hotel.value.ceo);
    formData.append('hotline', Hotel.value.hotline);
    api.postAPI('/Hotel', formData)
        .then((res) => {
            emits('Hotel-saved');
            $('#create-update-Hotel-modal').modal('hide');
        })
        .catch((error) => {
            console.error('Error creating Hotel:', error);
        });
    resetForm();
};

/** update Hotel */
const updateHotel = () => {
   
    const data = {
        hotelName: Hotel.value.hotelName,
        linkHotel: Hotel.value.linkHotel,
        rate: Hotel.value.rate,
        address: Hotel.value.address,
        ceo: Hotel.value.ceo,
        hotline: Hotel.value.hotline,
        id: props.editHotel.id,
    };

    api.putAPI(`/Hotel/${props.editHotel.id}`, data)
        .then((res) => {
            emits('Hotel-saved');
            $('#create-update-Hotel-modal').modal('hide');
        })
        .catch((error) => {
            console.error('Error updating Hotel:', error);
        });
    resetForm();
};

/**
 * handle submit
 */
const handleSubmit = () => {
    if (props.isEditMode) {
        updateHotel();
    } else {
        createHotel();
    }
};

/**
 * update data of edit Hotel
 */
watch(
    () => props.editHotel,
    (newVal) => {
        if (newVal) {
            Hotel.value = { ...newVal };
        }
    },
    { immediate: true },
);
</script>
