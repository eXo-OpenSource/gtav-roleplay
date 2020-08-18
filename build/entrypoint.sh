#!/bin/bash
# Enable core dump collection (https://dev.to/mizutani/how-to-get-core-file-of-segmentation-fault-process-in-docker-22ii)
echo '/tmp/core.%h.%e.%t' > /proc/sys/kernel/core_pattern
ulimit -c unlimited

# Start server
./altv-server --config=/opt/altv/server.cfg