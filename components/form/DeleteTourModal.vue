<template>
    <CVModal id_model="deleteTourModal">
        <template #icon>
            <slot name="icon"></slot>
        </template>
        <template #title>
            <slot name="header">Xóa Tour này</slot>
        </template>
        <template #body>
            <p>Bạn có chắc chắn muốn xóa Tour này?</p>
            <div class="modal-footer align-content-center justify-content-center">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">
                    Hủy bỏ
                </button>
                <button type="button" class="btn btn-danger" @click="deleteTour">
                    Xóa
                </button>
            </div>
        </template>
    </CVModal>
</template>
<script setup>
import Api from '~/service/Base/api.ts';

const props = defineProps({
    TourId: {
        type: String,
        required: true,
    },
});

const api = new Api();
const emits = defineEmits(['Tour-deleted', 'hide-modal']);
const deleteTour = () => {
    const id = props.TourId;
    api.deleteById('/Tour', id)
        .then((res) => {
            emits('Tour-deleted', id);
            emits('hide-modal');
            $('.btn-close').click();
        })
        .catch((error) => {
            console.error('Lỗi khi xóa hTour:', error);
        });
};
</script>

<style scoped></style>
