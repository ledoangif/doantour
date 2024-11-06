<template>
    <Form @submit="handleSubmit">
        <CVModal id_model="create-update-Tour-modal">
            <template #icon>
                <slot name="icon"></slot>
            </template>
            <template #title>
                <span v-if="!isEditMode">
                    <slot name="header">Thêm Tour du lịch </slot>
                </span>
                <span v-else>
                    <slot name="header">Cập nhật Tour du lịch</slot>
                </span>
            </template>
            <template #body>
                <div class="row">
                    <div class="row col-lg-6 mb-3 form-group required">
                        <div class="row mb-3 form-group required">
                            <label
                                for="source-name"
                                class="col-sm-4 col-form-label control-label text-end"
                            >
                                Ảnh
                            </label>
                            <div class="col-sm-8">
                                <Field
                                    name="image"
                                    v-model="Tour.image"
                                    type="file"
                                    class="form-control"
                                    @change="handlefiles"
                                />
                            </div>
                        </div>
                        <div class="row form-group required">
                            <label
                                for="source-name"
                                class="col-sm-4 col-form-label control-label text-end"
                            >
                                SL Vé
                            </label>
                            <div class="col-sm-8">
                                <Field
                                    id="seat"
                                    name="seat"
                                    v-model="Tour.seat"
                                    class="form-control"
                                />
                                <br />
                            </div>
                        </div>
                        <div class="row mb-3 form-group required">
                            <label
                                for="source-name"
                                class="col-sm-4 col-form-label control-label text-end"
                            >
                                Giá tiền
                            </label>
                            <div class="col-sm-8">
                                <Field
                                    id="cost"
                                    name="cost"
                                    v-model="computedValue"
                                    class="form-control"
                                    @click="handleInput"
                                />
                            </div>
                        </div>
                        <div class="row mb-3 form-group required">
                            <label
                                for="source-name"
                                class="col-sm-4 col-form-label control-label text-end"
                            >
                                Giảm giá
                            </label>
                            <div class="col-sm-8">
                                <Field
                                    name="discount"
                                    v-model="Tour.discount"
                                    type="text"
                                    class="form-control"
                                />
                            </div>
                        </div>
                        <div class="row mb-3 form-group required">
                            <label
                                for="source-name"
                                class="col-sm-4 col-form-label control-label text-end"
                            >
                                Nội địa?
                            </label>
                            <div class="col-sm-8">
                                <Field
                                    as="select"
                                    id="isLocal"
                                    name="isLocal"
                                    v-model="Tour.isLocal"
                                    class="form-control"
                                >
                                    <option
                                        v-for="item in isLocal"
                                        class="text-dark"
                                        :value="item.value"
                                    >
                                        {{ item.value }}
                                    </option>
                                </Field>
                            </div>
                        </div>
                        <div class="row mb-3 form-group required">
                            <label
                                for="source-name"
                                class="col-sm-4 col-form-label control-label text-end"
                            >
                                Quốc Gia
                            </label>
                            <div class="col-sm-8">
                                <Field
                                    as="select"
                                    id="country"
                                    name="country"
                                    v-model="Tour.countryId"
                                    class="form-control"
                                >
                                    <option
                                        v-for="item in Country"
                                        class="text-dark"
                                        :value="item.id"
                                    >
                                        {{ item.countryName }}
                                    </option>
                                </Field>
                            </div>
                        </div>
                        <div class="row mb-3 form-group required">
                            <label
                                for="source-name"
                                class="col-sm-4 col-form-label control-label text-end"
                            >
                                Địa điểm
                            </label>
                            <div class="col-sm-8">
                                <Field
                                    name="place"
                                    v-model="Tour.place"
                                    type="text"
                                    class="form-control"
                                />
                            </div>
                        </div>
                        <div class="row mb-3 form-group required">
                            <label
                                for="source-name"
                                class="col-sm-4 col-form-label control-label text-end"
                            >
                                Nơi khởi hành
                            </label>
                            <div class="col-sm-8">
                                <Field
                                    name="placeStart"
                                    v-model="Tour.placeStart"
                                    type="text"
                                    class="form-control"
                                />
                            </div>
                        </div>
                        <div class="row mb-3 form-group required">
                            <label
                                for="source-name"
                                class="col-sm-4 col-form-label control-label text-end"
                            >
                                Nơi tập trung
                            </label>
                            <div class="col-sm-8">
                                <Field
                                    name="meetingPoint"
                                    v-model="Tour.meetingPoint"
                                    type="text"
                                    class="form-control"
                                />
                            </div>
                        </div>
                        <div class="row mb-3 form-group required">
                            <label
                                for="source-name"
                                class="col-sm-4 col-form-label control-label text-end"
                            >
                                Nơi kết thúc
                            </label>
                            <div class="col-sm-8">
                                <Field
                                    name="placeEnd"
                                    v-model="Tour.placeEnd"
                                    type="text"
                                    class="form-control"
                                />
                            </div>
                        </div>
                        <div class="row mb-3 form-group required">
                            <label
                                for="source-name"
                                class="col-sm-4 col-form-label control-label text-end"
                            >
                                Ngày bắt đầu
                            </label>
                            <div class="col-sm-8 d-flex flex-row">
                                <span class="col-4">
                                    <Field
                                        name="timeStart"
                                        v-model="Tour.timeStart"
                                        type="time"
                                        class="input-edit me-1 form-control"
                                    />
                                </span>
                                <span class="col-8 ms-2">
                                    <Field
                                        name="dateStart"
                                        v-model="Tour.dateStart"
                                        type="date"
                                        class="form-control"
                                    />
                                </span>
                            </div>
                        </div>
                        <div class="row mb-3 form-group required">
                            <label
                                for="source-name"
                                class="col-sm-4 col-form-label control-label text-end"
                            >
                                Ngày kết thúc
                            </label>
                            <div class="col-sm-8 d-flex flex-row">
                                <span class="col-4">
                                    <Field
                                        name="timeEnd"
                                        v-model="Tour.timeEnd"
                                        type="time"
                                        class="input-edit me-1 form-control"
                                /></span>
                                <span class="col-8 ms-2">
                                    <Field
                                        name="dateEnd"
                                        v-model="Tour.dateEnd"
                                        type="date"
                                        class="form-control"
                                    />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row col-lg-6">
                        <div class="row form-group required mb-3">
                            <label
                                for="source-name"
                                class="col-sm-3 col-form-label control-label text-end"
                                >Tên Tour
                            </label>
                            <div class="col-9">
                                <textarea
                                    name="nameTour"
                                    v-model="Tour.nameTour"
                                    type="text"
                                    class="form-control"
                                    style="height: 50px"
                                />
                            </div>
                        </div>
                        <div class="row form-group required mb-3">
                            <label
                                for="source-name"
                                class="col-sm-3 col-form-label control-label text-end"
                                >Mô tả
                            </label>
                            <div class="col-sm-9">
                                <textarea
                                    name="descripttion"
                                    v-model="Tour.descripttion"
                                    type="text"
                                    class="form-control"
                                    style="height: 150px"
                                />
                            </div>
                        </div>
                        <div class="row form-group required mb-3">
                            <label
                                for="source-name"
                                class="col-sm-3 col-form-label control-label text-end"
                                >Dịch vụ bao gồm
                            </label>
                            <div class="col-sm-9">
                                <TipTap v-model="Tour.serviceInclude"></TipTap>
                            </div>

                            <!-- <input type="text" class="input-edit "
                                                        v-model="request.cvNote" /> -->
                        </div>
                        <div class="row form-group required mb-3">
                            <label
                                for="source-name"
                                class="col-sm-3 col-form-label control-label text-end"
                                >Dịch vụ không bao gồm
                            </label>
                            <div class="col-sm-9">
                                <TipTap v-model="Tour.serviceNotInclude"></TipTap>
                            </div>

                            <!-- <input type="text" class="input-edit "
                                                        v-model="request.cvNote" /> -->
                        </div>
                        <div class="row form-group required mb-3">
                            <label
                                for="source-name"
                                class="col-sm-3 col-form-label control-label text-end"
                                >Lịch Trình
                            </label>
                            <div class="col-sm-9">
                                <TipTap v-model="Tour.plan"></TipTap>
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
import { Form, Field } from 'vee-validate';
const api = new Api();
const Country = ref([]);
const formRef = ref(null);
const emits = defineEmits(['Tour-saved']);
const accountId = ref('');
/** received data */
const props = defineProps({
    editTour: {
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
const Tour = ref({
    nameTour: '',
    descripttion: '',
    seat: '',
    cost: '',
    discount: '',
    dateEnd: '',
    dateStart: '',
    placeEnd: '',
    placeStart: '',
    timeEnd: '',
    timeStart: '',
    place: '',
    plan: '',
    image: '',
    serviceInclude: '',
    serviceNotInclude: '',
    countryId: '',
    isLocal: '',
    meetingPoint: '',
});
/** reset form */
const resetForm = () => {
    if (formRef.value) {
        formRef.value.resetForm();
    }
};
const handlefiles = async (event) => {
    const files = event.target.files;
    const formData = new FormData();
    formData.append('ImageFile', files[0]);
    var ImageResponse = await api.postAPI('/Tour/UploadFile', formData);
    Tour.value.image = ImageResponse.data;
};

const isLocal = [
    { id: 1, value: true },
    { id: 2, value: false },
];
/**
 * define Tour
 */
if(process.client)
{
    accountId.value = localStorage.getItem('userId');
}
const fetchData = async () => {

    try {
        const resCountry = await api.get(`/Country`, null);
        Country.value = resCountry.data.responseData;
    } catch (error) {
        console.error('Error fetching countries:', error);
    }
};
onMounted(async () => {
    
await fetchData();

})
/** validate */
/**
 * insert Tour
 */
// Định nghĩa Vue Directive
const createTour = () => {
    const formData2 = new FormData();
    formData2.append('nameTour', Tour.value.nameTour);
    formData2.append('descripttion', Tour.value.descripttion);
    formData2.append('seat', Tour.value.seat);
    const cleanCost = parseFloat(Tour.value.cost.replace(/[\D\s\._\-]+/g, ''));
    formData2.append('cost', isNaN(cleanCost) ? 0 : cleanCost);
    formData2.append('discount', Tour.value.discount);
    formData2.append('islocal', Tour.value.isLocal);
    formData2.append('dateEnd', Tour.value.dateEnd);
    formData2.append('dateStart', Tour.value.dateStart);
    formData2.append('plan', Tour.value.plan);
    formData2.append('image', Tour.value.image);
    formData2.append('serviceInclude', Tour.value.serviceInclude);
    formData2.append('serviceNotInclude', Tour.value.serviceNotInclude);
    formData2.append('countryId', Tour.value.countryId);
    formData2.append('timeStart', Tour.value.timeStart);
    formData2.append('timeEnd', Tour.value.timeEnd);
    formData2.append('placeStart', Tour.value.placeStart);
    formData2.append('placeEnd', Tour.value.placeEnd);
    formData2.append('place', Tour.value.place);
    formData2.append('meetingPoint', Tour.value.meetingPoint);
    formData2.append('appUserId',accountId.value);
    api.postAPI('/Tour/InsertTour', formData2)
        .then((res) => {
            emits('Tour-saved');
            $('#create-update-Tour-modal').modal('hide');
        })
        .catch((error) => {
            console.error('Error creating Tour:', error);
        });
    resetForm();
};

/** update Tour */
const updateTour = async () => {
    // Ensure cost is treated as a string
    const costStr = Tour.value.cost ? String(Tour.value.cost) : '';
    const cleanCost = parseFloat(costStr.replace(/[^0-9.-]+/g, ''));
    const data = {
        nameTour: Tour.value.nameTour || '',
        descripttion: Tour.value.descripttion || '',
        seat: Tour.value.seat || '',
        email: Tour.value.email || '',
        cost: isNaN(cleanCost) ? 0 : cleanCost,
        isLocal:JSON.parse( Tour.value.isLocal) || false,
        dateEnd: Tour.value.dateEnd || '',
        discount: Tour.value.discount || 0,
        dateStart: Tour.value.dateStart || '',
        plan: Tour.value.plan || '',
        image: Tour.value.image || '',
        serviceInclude: Tour.value.serviceInclude || '',
        serviceNotInclude: Tour.value.serviceNotInclude || '',
        countryId: Tour.value.countryId || '',
        timeEnd: Tour.value.timeEnd || '',
        timeStart: Tour.value.timeStart || '',
        placeEnd: Tour.value.placeEnd || '',
        placeStart: Tour.value.placeStart || '',
        place: Tour.value.place || '',
        meetingPoint: Tour.value.meetingPoint || '',
        appUserId: props.editTour.appUserId,
        id: props.editTour.id,
    };

    await api
        .putAPI(`/Tour/${props.editTour.id}`, data)
        .then((res) => {
            emits('Tour-saved');
            $('#create-update-Tour-modal').modal('hide');
        })
        .catch((error) => {
            console.error('Error updating Tour:', error);
        });
    resetForm();
};

/**
 * handle submit
 */
const handleSubmit = () => {
    if (props.isEditMode) {
        updateTour();
    } else {
        createTour();
    }
};

/**
 * update data of edit Tour
 */
watch(
    () => props.editTour,
    (newVal) => {
        if (newVal) {
            Tour.value = { ...newVal };
            Tour.value.dateStart = props.editTour.dateStart
                ? props.editTour.dateStart.split('T')[0]
                : '';
            Tour.value.dateEnd = props.editTour.dateEnd
                ? props.editTour.dateEnd.split('T')[0]
                : '';
        }
    },
    { immediate: true },
);
const computedValue = computed({
    get: () => {
        var input = String(Tour.value.cost || '').replace(/[\D\s\._\-]+/g, '');
        input = input ? parseFloat(input, 10) : 0;
        return input.toLocaleString('en-US');
    },
    set: (newValue) => {
        newValue = String(newValue).replace(/[^\d.-]+/g, '');
        newValue = newValue ? parseFloat(newValue, 10) : 0;
        let isNumber = !isNaN(parseFloat(newValue)) && isFinite(newValue);

        if (newValue == '') {
            Tour.value.cost = '';
        }

        if (isNumber && parseFloat(newValue) > 0) {
            Tour.value.cost = newValue.toLocaleString('en-US');
        }
    },
});

/**
 * Reset input value after computed execute if it's not number
 * @param {*} event
 */
 const handleInput = (event) => {
    const inputValue = event.target.value;
    const cleanedValue = inputValue.replace(/[^\d.]/g, ''); // Chỉ giữ lại các ký tự số và dấu chấm
    event.target.value = cleanedValue;
};

</script>
